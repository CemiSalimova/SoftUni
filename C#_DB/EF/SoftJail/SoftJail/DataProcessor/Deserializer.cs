namespace SoftJail.DataProcessor
{

    using Data;
    using Newtonsoft.Json;
    using SoftJail.Data.Models;
    using SoftJail.Data.Models.Enums;
    using SoftJail.DataProcessor.ImportDto;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.Json.Serialization;
    using System.Xml.Serialization;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid Data";

        private const string SuccessfullyImportedDepartmentsWithCells
            = "Imported {0} with {1} cells";

        private const string SuccessfullyImportedPrisonersWithMails
            = "Imported {0} {1} years old";
        private const string SuccessfullyImportedOfficerPrisons
           = "Imported {0} ({1} prisoners)";
        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            var departmentsDto = JsonConvert.DeserializeObject<List<ImportDepartmentDto>>(jsonString);
            List<Department> departmentsDb = new List<Department>();

            foreach (var depdto in departmentsDto)
            {
                if (!IsValid(depdto) || depdto.Cells.Count == 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                var depDb = new Department
                {
                    Name = depdto.Name
                };
                foreach (var cellDto in depdto.Cells)
                {
                    if (!IsValid(cellDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        break;
                    }
                    
                        Cell cellDb = new Cell
                        {
                            CellNumber = cellDto.CellNumber,
                            HasWindow= cellDto.HasWindow
                        };
                        depDb.Cells.Add(cellDb);
              
                }
                if (depDb.Cells.Count == 0)
                {
                    continue;
                }
                else
                {
                    departmentsDb.Add(depDb);
                    sb.AppendLine(String.Format(SuccessfullyImportedDepartmentsWithCells, depDb.Name, depDb.Cells.Count));
                }
            }

            context.Departments.AddRange(departmentsDb);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            var prisonersDto = JsonConvert.DeserializeObject<List<ImportprisonerDto>>(jsonString);
            List<Prisoner> prisonersDb = new List<Prisoner>();
            //List<Mail> mailsDb = new List<Mail>();
            bool IsAddressValid = true;
            foreach (var prisDto in prisonersDto)
            {

                var checkedIncarcerationDate = DateTime.TryParseExact(prisDto.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dbIncarcerationDate);

                if (!checkedIncarcerationDate)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime? releaseDate = null;
                if (!String.IsNullOrEmpty(prisDto.ReleaseDate))
                {
                    DateTime releaseDateValue;
                    bool isReleaseDateValid = DateTime.TryParseExact(prisDto.ReleaseDate, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out releaseDateValue);

                    if (!isReleaseDateValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    releaseDate = releaseDateValue;
                }
                if (!IsValid(prisDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                else
                {
                    Prisoner prisonerDb = new Prisoner
                    {
                        FullName = prisDto.FullName,
                        Nickname = prisDto.Nickname,
                        Age = prisDto.Age,
                        IncarcerationDate = dbIncarcerationDate,
                        ReleaseDate = releaseDate,
                        Bail = prisDto.Bail,
                        CellId = prisDto.CellId
                    }; 

                    foreach (var mailDto in prisDto.Mails)
                    {
                        IsAddressValid = true;

                        if (!IsValid(mailDto))
                        {
                            sb.AppendLine(ErrorMessage);
                            IsAddressValid = false;
                            continue;
                        }
                        
                        Mail mailDb = new Mail
                            {
                                Description = mailDto.Description,
                                Sender = mailDto.Sender,
                                Address = mailDto.Address
                            };
                          //  mailsDb.Add(mailDb);
                        prisonerDb.Mails.Add(mailDb);
                    }
                    
                    if (!IsAddressValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    else
                    {
                        prisonersDb.Add(prisonerDb);
                        sb.AppendLine(String.Format(SuccessfullyImportedPrisonersWithMails, prisonerDb.FullName, prisonerDb.Age));
                    };
                }

            }

            context.Prisoners.AddRange(prisonersDb);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            XmlRootAttribute xmlRoot = new XmlRootAttribute("Officers");
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportOfficerDto[]), xmlRoot);
            using StringReader sr = new StringReader(xmlString);
            ImportOfficerDto[] officersDtos = (ImportOfficerDto[])xmlSerializer.Deserialize(sr);
            HashSet<Officer> officersDb = new HashSet<Officer>();
            foreach (var officerDto in officersDtos)
            {
                if (!IsValid(officerDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                var chechPositionDto = Enum.TryParse<Position>(officerDto.Position, out Position PositionDto);
                var chechWeaponDto = Enum.TryParse<Weapon>(officerDto.Weapon, out Weapon WeaponDto);
                var checkDepId = context.Departments.FirstOrDefault(d => d.Id == officerDto.DepartmentId);
                if (checkDepId == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                if (!chechPositionDto )
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                };
                if (!chechWeaponDto)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                };
                
                Officer officerDb = new Officer
                {
                    FullName = officerDto.Name,
                    Salary = officerDto.Money,
                    Position = PositionDto,
                    Weapon = WeaponDto,
                    DepartmentId = officerDto.DepartmentId

                };
                
                foreach (var prisonerId in officerDto.PrisonersIds)
                {
                    
                    officerDb.OfficerPrisoners.Add(
                        new OfficerPrisoner {
                            Officer = officerDb,
                            PrisonerId = prisonerId.PrisonerId
                        });

                    
                };
                officersDb.Add(officerDb);
                sb.AppendLine(String.Format(SuccessfullyImportedOfficerPrisons, officerDb.FullName, officerDb.OfficerPrisoners.Count()));
            }
          
            context.Officers.AddRange(officersDb);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return isValid;
        }
    }
}