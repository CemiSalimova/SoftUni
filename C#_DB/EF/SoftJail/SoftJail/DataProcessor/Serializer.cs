namespace SoftJail.DataProcessor
{

    using Data;
    using Newtonsoft.Json;
    using SoftJail.DataProcessor.ExportDto;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    public class Serializer
    {
        private static object xmlSerializer;

        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            var prisenors = context.Prisoners
                .ToList()
                 .Where(a => ids.Contains(a.Id))
                 .Select(pr => new
                 {
                     Id = pr.Id,
                     Name = pr.FullName,
                     CellNumber = pr.Cell.CellNumber,
                     Officers = pr.PrisonerOfficers
                     .Select(r => new
                     {
                         OfficerName = r.Officer.FullName,
                         Department = r.Officer.Department.Name
                     })
                     .OrderBy(off=>off.OfficerName)
                     .ToList(),
                     TotalOfficerSalary = Math.Round( pr.PrisonerOfficers
                     .Where(b=> ids.Contains(b.PrisonerId))
                    .Sum(a=>a.Officer.Salary),2)
                

                 }) 
                 .OrderBy(p=>p.Name)
                 .ThenBy(pp=>pp.Id)
                 .ToList();
            var result = JsonConvert.SerializeObject(prisenors,Formatting.Indented);
            return result;


        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            StringBuilder sb = new StringBuilder();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExportPrisonerDto[]), new XmlRootAttribute("Prisoners"));
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            var namesPr = prisonersNames.Split(",").ToArray();

            var prisoners = context.Prisoners
                .Where(pr => namesPr.Contains(pr.FullName))
                .Select(p => new ExportPrisonersDto
                {
                    Id = p.Id,
                    Name = p.FullName,
                    IncarcerationDate = p.IncarcerationDate.ToString("yyyy-MM-dd”", CultureInfo.InvariantCulture),
                    Mails = p.Mails.Select(d => new ExportPrisonersMessegesDto
                    {
                        Description = String.Join(" ", d.Description.Reverse())
                    }).ToList()

                }) ;
            using (StringWriter stringWriter = new StringWriter(sb))
            {
                xmlSerializer.Serialize(stringWriter, prisoners, namespaces);
            }

            return sb.ToString().TrimEnd();
        }
    }
}