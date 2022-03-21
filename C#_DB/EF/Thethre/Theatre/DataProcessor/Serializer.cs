namespace Theatre.DataProcessor
{
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Theatre.Data;
    using Theatre.DataProcessor.ExportDto;

    public class Serializer
    {
        public static string ExportTheatres(TheatreContext context, int numbersOfHalls)
        {
            var theatres = context.Theatres.ToList()
                .Where(h => h.NumberOfHalls > numbersOfHalls)
                .Where(c => c.Tickets.Count() >= 20)
                .Where(r => r.Tickets.Any(rn => rn.RowNumber >= 1 && rn.RowNumber <= 5))
                .Select(t => new
                {
                    Name = t.Name,
                    Halls = t.NumberOfHalls,
                    TotalIncome = context.Tickets.Where(rn => rn.RowNumber >= 1 && rn.RowNumber <= 5 && rn.Theatre.Name == t.Name).Sum(pr => pr.Price),
                    Tickets = t.Tickets
                    .Where(r => r.RowNumber >= 1 && r.RowNumber <= 5)
                    .Select(tic => new
                    {
                        Price = Math.Round(tic.Price, 2),
                        RowNumber = tic.RowNumber
                    })
                    .
                    OrderByDescending(ttt => ttt.Price)
                    .ToList()
                }) 
                .OrderByDescending(t=>t.Halls)
                .ThenBy(t=>t.Name);
            var rezult = JsonConvert.SerializeObject(theatres,Formatting.Indented);
            return rezult;
        }

        public static string ExportPlays(TheatreContext context, double rating)
        {
            StringBuilder sb = new StringBuilder();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExportPlayDto[]), new XmlRootAttribute("Plays"));
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            if (rating==0)
            {
                sb.AppendLine("Premier");
            }
            var plays=context.Plays.ToList()
                .Where(r=>r.Rating>= rating)
            using (StringWriter stringWriter = new StringWriter(sb))
            {
                xmlSerializer.Serialize(stringWriter, plays, namespaces);
            }

            return sb.ToString().TrimEnd();
        }
    }
}
