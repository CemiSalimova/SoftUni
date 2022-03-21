namespace VaporStore.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.Data.Models.Enums;
    using VaporStore.DataProcessor.Dto.Export;

    public static class Serializer
    {
        public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
        {

            StringBuilder sb = new StringBuilder();
            string result;

            var genres = context.Genres
                .ToList()
                .Where(a => genreNames.Contains(a.Name))
                .Select(b => new
                {
                    Id = b.Id,
                    Genre = b.Name,
                    Games = b.Games
                    .Select(ga => new
                    {
                        Id = ga.Id,
                        Title = ga.Name,
                        Developer = ga.Developer.Name,
                        Tags = $"{String.Join(", ",ga.GameTags.Select(q => q.Tag.Name))}",
                        Players = ga.Purchases.Count()
                    })
                    .Where(a => a.Players > 0)
                    .OrderByDescending(ga => ga.Players)
                    .ThenBy(ga => ga.Id)
                    .ToList(),
                    TotalPlayers = b.Games.SelectMany(a => a.Purchases).Count()
                })
                .OrderByDescending(g => g.TotalPlayers)
            .ThenBy(g => g.Id).ToList();
            result = JsonConvert.SerializeObject(genres, Formatting.Indented);
            return result;
        }

        public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
        {
            StringBuilder sb = new StringBuilder();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExportUserDto[]), new XmlRootAttribute("Users"));
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            using StringWriter stringWriter = new StringWriter(sb);
            PurchaseType purchaseType = Enum.Parse<PurchaseType>(storeType);

            var users = context.Users
                .ToArray()
                 .Where(u => u.Cards.Any(c => c.Purchases.Any()))
                .Select(a => new ExportUserDto()
                {
                    Username = a.Username,
                    Purchases = context.Purchases
                    .ToArray()
                                .Where(c => c.Card.User.Username == a.Username && c.Type == purchaseType)
                                 .OrderBy(p => p.Date)
                                .Select(b => new ExportUserPurchaseDto()
                                {
                                    CardNumber = b.Card.Number,
                                    Cvc = b.Card.Cvc,
                                    PurchaseDate = b.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                                    Game = new ExportUserPurchaseGameDto
                                    {
                                        GameName = b.Game.Name,
                                        Genre = b.Game.Genre.Name,
                                        Price = b.Game.Price
                                    }


                                })
                                .ToArray(),
                    TotalSpent = context
                    .Purchases
                    .ToArray()
                    .Where(c => c.Card.User.Username== a.Username && c.Type == purchaseType).
                    Sum(p => p.Game.Price)
                    
                })
                .Where(u => u.Purchases.Length > 0)
                .OrderByDescending(u => u.TotalSpent)
                .ThenBy(u => u.Username)
                .ToArray();

                    xmlSerializer.Serialize(stringWriter, users, namespaces);

            return sb.ToString().TrimEnd();
            
        }
    }
}