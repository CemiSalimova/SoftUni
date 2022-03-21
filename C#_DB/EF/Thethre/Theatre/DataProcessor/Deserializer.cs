namespace Theatre.DataProcessor
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;
    using Theatre.Data;
    using Theatre.Data.Models;
    using Theatre.Data.Models.Enums;
    using Theatre.DataProcessor.ImportDto;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfulImportPlay
            = "Successfully imported {0} with genre {1} and a rating of {2}!";

        private const string SuccessfulImportActor
            = "Successfully imported actor {0} as a {1} character!";

        private const string SuccessfulImportTheatre
            = "Successfully imported theatre {0} with #{1} tickets!";

        public static string ImportPlays(TheatreContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            XmlRootAttribute xmlRoot = new XmlRootAttribute("Plays");
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportPlayDto[]), xmlRoot);
            using StringReader sr = new StringReader(xmlString);
            ImportPlayDto[] playsDto = (ImportPlayDto[])xmlSerializer.Deserialize(sr);
            List<Play> playsDb = new List<Play>();

            foreach (var playDto in playsDto)
            {
                var checkDur = TimeSpan.TryParseExact(playDto.Duration, "c", CultureInfo.InvariantCulture, TimeSpanStyles.None, out TimeSpan dbDuration);
                var checkGenre = Enum.TryParse<Genre>(playDto.Genre, out Genre dbGenre);

                if (!checkDur)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                if (!checkGenre)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                if (playDto.Rating < 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                if (dbDuration.TotalHours < 1)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                if (!IsValid(playDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Play playDb = new Play
                {
                    Title = playDto.Title,
                    Duration = dbDuration,
                    Rating = playDto.Rating,
                    Genre = dbGenre,
                    Description = playDto.Description,
                    Screenwriter = playDto.Screenwriter
                };

                playsDb.Add(playDb);
                sb.AppendLine(String.Format(SuccessfulImportPlay, playDb.Title, playDb.Genre, playDb.Rating));
            }

            context.Plays.AddRange(playsDb);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportCasts(TheatreContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            XmlRootAttribute xmlRoot = new XmlRootAttribute("Casts");
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportCastDto[]), xmlRoot);
            using StringReader sr = new StringReader(xmlString);
            ImportCastDto[] castsDto = (ImportCastDto[])xmlSerializer.Deserialize(sr);
            List<Cast> castsDb = new List<Cast>();
            foreach (var castDto in castsDto)
            {
                if (!IsValid(castDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Cast castDb = new Cast
                {
                    FullName = castDto.FullName,
                    IsMainCharacter = castDto.IsMainCharacter,
                    PhoneNumber = castDto.PhoneNumber,
                    PlayId = castDto.PlayId
                };
                castsDb.Add(castDb);

                sb.AppendLine(String.Format(SuccessfulImportActor, castDb.FullName, castDb.IsMainCharacter == true ? "main" : "lesser"));
            }

            context.Casts.AddRange(castsDb);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportTtheatersTickets(TheatreContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            var theatresDto = JsonConvert.DeserializeObject<List<ImportTheatreDto>>(jsonString);
            List<Theatre> theatresDb = new List<Theatre>();
            
            foreach (var theatreDto in theatresDto)
            {
                if (!IsValid(theatreDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;

                }
                Theatre theathreDb = new Theatre
                {
                    Name = theatreDto.Name,
                    NumberOfHalls = theatreDto.NumberOfHalls,
                    Director = theatreDto.Director
                };
                foreach (var ticketDto in theatreDto.Tickets)
                {
                    if (!IsValid(ticketDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    if (ticketDto.Price<=1)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    Ticket ticketDb = new Ticket
                    {
                        Price = ticketDto.Price,
                        RowNumber = ticketDto.RowNumber,
                        PlayId = ticketDto.PlayId
                    };
                    theathreDb.Tickets.Add(ticketDb);
                }
                
                theatresDb.Add(theathreDb);
                sb.AppendLine(String.Format(SuccessfulImportTheatre, theathreDb.Name, theathreDb.Tickets.Count));

            }
           
            context.Theatres.AddRange(theatresDb);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }


        private static bool IsValid(object obj)
        {
            var validator = new ValidationContext(obj);
            var validationRes = new List<ValidationResult>();

            var result = Validator.TryValidateObject(obj, validator, validationRes, true);
            return result;
        }
    }
}
