namespace ProductShop
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Microsoft.EntityFrameworkCore;

    using Data;

    using ProductShop.Models;

    public class DbInitializer
    {
        public static void ResetDatabase(ProductShopContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

           Console.WriteLine("MusicHub database created successfully.");

            Seed(context);
        }

        private static void Seed(ProductShopContext context)
        {
            var datasetsJson = users.json;
            
            var datasets = JsonConvert.DeserializeObject<Dictionary<string, IEnumerable<JObject>>>(datasetsJson);

            foreach (var dataset in datasets)
            {
                var entityType = GetType(dataset.Key);

                using (var transaction = context.Database.BeginTransaction())
                {
                    var entities = dataset.Value
                        .Select(j => j.ToObject(entityType))
                        .ToArray();
                    var entityName = $"{entityType.Name}s";
                    context.AddRange(entities);

                    if (entityType != typeof(CategoryProduct))
                    {
                        context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT " + entityName + " ON;");
                    }

                    context.SaveChanges();

                    if (entityType != typeof(CategoryProduct))
                    {
                        context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT " + entityName + "  OFF;");
                    }
                    transaction.Commit();
                }
            }
        }

        private static Type GetType(string modelName)
        {
            var modelType = Assembly
                .GetEntryAssembly()?
                .GetTypes()
                .FirstOrDefault(t => t.Name == modelName);

            return modelType;
        }
    }
}
