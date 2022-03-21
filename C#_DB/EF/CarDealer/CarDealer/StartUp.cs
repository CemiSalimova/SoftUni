using AutoMapper;
using CarDealer.Data;
using System;
using System.IO;
using Newtonsoft.Json;
using CarDealer.DTO;
using System.Collections.Generic;
using CarDealer.Models;
using System.Linq;
using Newtonsoft.Json.Serialization;
using CarDealer.DTO.Inputs;
using CarDealer.DTO.Outputs;
using Newtonsoft.Json.Converters;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using CarDealer.DTO.Outputs.CarsWithProductOutputDtos;

namespace CarDealer
{
    public class StartUp
    {
        private static IMapper mapper;
        static void Main(string[] args)
        {
            var context = new CarDealerContext();
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();
            //var suppliersFromJsonFile = File.ReadAllText("../../../Datasets/suppliers.json");
            //var partsFromJsonFile = File.ReadAllText("../../../Datasets/parts.json");
            //var carsFromJsonFile = File.ReadAllText("../../../Datasets/cars.json");
            //var customersFromJsonFile = File.ReadAllText("../../../Datasets/customers.json");
            //var salesFromJsonFile = File.ReadAllText("../../../Datasets/sales.json");


            //Console.WriteLine(ImportSuppliers(context, suppliersFromJsonFile));
            //Console.WriteLine(ImportParts(context, partsFromJsonFile));
            //Console.WriteLine(ImportCars(context, carsFromJsonFile));
            //Console.WriteLine(ImportCustomers(context, customersFromJsonFile));
            //Console.WriteLine(ImportSales(context, salesFromJsonFile));
            //////Console.WriteLine(carsFromJsonFile);
            //Console.WriteLine(GetOrderedCustomers(context));
            // Console.WriteLine(GetCarsFromMakeToyota(context));
            // Console.WriteLine(GetLocalSuppliers(context));
            Console.WriteLine(GetCarsWithTheirListOfParts(context));
            //Console.WriteLine(GetSalesWithAppliedDiscount(context));
           // Console.WriteLine(GetTotalSalesByCustomer(context));
        }
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var suppliers = JsonConvert.DeserializeObject<IEnumerable<SuppliersInputDto>>(inputJson);
            InitializeMapper();
            var mappingSupliers = mapper.Map<IEnumerable<Supplier>>(suppliers);
            context.Suppliers.AddRange(mappingSupliers);
            context.SaveChanges();
            return $"Successfully imported {mappingSupliers.Count()}.";
        }
        public static string ImportParts(CarDealerContext context, string inputJson)
        {

            InitializeMapper();
            var partsFromJson = JsonConvert.DeserializeObject<IEnumerable<PartsInputDto>>(inputJson);
            var checkSupplierId = context.Suppliers.Select(a => a.Id).ToList();
            var parts = new List<Part>();
            foreach (PartsInputDto part in partsFromJson)
            {
                if (checkSupplierId.Contains(part.SupplierId))
                {
                    Part newpart = mapper.Map<Part>(part);
                    parts.Add(newpart);
                }

            }

            context.Parts.AddRange(parts);
            context.SaveChanges();
            return $"Successfully imported {parts.Count()}.";

        }
        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var carsJson = JsonConvert.DeserializeObject<IEnumerable<CarsInputDto>>(inputJson);
            var cars = new List<Car>();
            foreach (var c in carsJson)
            {
                Car car = new Car
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance,
                };
                foreach (var p in c.PartsId.Distinct())
                {
                    car.PartCars.Add
                        (
                        new PartCar
                        {
                            Car = car,
                            PartId = p
                        });
                };
                cars.Add(car);
            }

            InitializeMapper();
            var mappingcars = mapper.Map<IEnumerable<Car>>(cars);
            context.AddRange(cars);
            context.SaveChanges();
            return $"Successfully imported {cars.Count()}.";



        }

        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {

            var customers = JsonConvert.DeserializeObject<IEnumerable<Customer>>(inputJson);
            InitializeMapper();
            var mappedCustomers = mapper.Map<IEnumerable<Customer>>(customers);
            context.Customers.AddRange(mappedCustomers);
            context.SaveChanges();
            return $"Successfully imported {mappedCustomers.Count()}.";
        }

        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var salesJson = JsonConvert.DeserializeObject<List<Sale>>(inputJson);

            InitializeMapper();
            context.Sales.AddRange(salesJson);
            context.SaveChanges();
            return $"Successfully imported {salesJson.Count()}.";

        }
        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var customers = context.Customers.OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver).ToList();
            InitializeMapper();

            var mappedcustomers = mapper.Map<IEnumerable<CustomersOrderOutputDto>>(customers);

            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = contractResolver,
                DateFormatString = "dd/MM/yyyy"
            };
            var rezult = JsonConvert.SerializeObject(mappedcustomers, jsonSettings);

            return rezult;
        }
        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var searchString = "Toyota";
            InitializeMapper();
            var cars = context.Cars
                .Where(c => c.Make == searchString)
                .OrderBy(b => b.Model)
                .ThenByDescending(a => a.TravelledDistance)
                .ProjectTo<CarsFromModelOutputDto>(mapper.ConfigurationProvider)
                .ToList();
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = contractResolver,
            };
            var rezult = JsonConvert.SerializeObject(cars, jsonSettings);
            return rezult;
        }
        public static string GetLocalSuppliers(CarDealerContext context)
        {
            InitializeMapper();
            var localSupplier = context.Suppliers
                .Where(y => y.IsImporter == false)
                .ProjectTo<LocalSupplierOutputDto>(mapper.ConfigurationProvider)
                .ToList();
            var rezult = JsonConvert.SerializeObject(localSupplier, Formatting.Indented);
            return rezult;
        }
        //public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        //{
        //    InitializeMapper();

        //    var cars = context.Cars
        //        .Select(a => new
        //        {
        //            car = new
        //            {
        //                Make = a.Make,
        //                Model = a.Model,
        //                TravelledDistance = a.TravelledDistance
        //            },
        //            parts = a.PartCars.Select(b => new
        //            {
        //                Name = b.Part.Name,
        //                Price = b.Part.Price.ToString("f2")
        //            })
        //        });


        //    var newRezult = JsonConvert.SerializeObject(cars, Formatting.Indented);
        //    return newRezult;
        //}
        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            InitializeMapper();
            var cars = context.Cars
                .ProjectTo<CarAndPartsDto>(mapper.ConfigurationProvider)
                .ToList();
           
            var newRezult = JsonConvert.SerializeObject(cars, Formatting.Indented);
            return newRezult;
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
           
            var customers = context.Customers
               
                .Select(a => new
                {
                    fullName = a.Name,
                    boughtCars = a.Sales.Count(),
                    spentMoney = a.Sales
                    .Sum(w => w.Car.PartCars.Sum(s => s.Part.Price))
                })
               .ToList();
            
               
            var rezult = JsonConvert.SerializeObject(customers, Formatting.Indented);
            return rezult;
        }

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            InitializeMapper();
            var sales = context.Sales.Take(10)
                .Select(a => new
                {
                    car = new
                    {
                        Make = a.Car.Make,
                        Model = a.Car.Model,
                        TravelledDistance = a.Car.TravelledDistance,
                    },
                    customerName = a.Customer.Name,
                    Discount = a.Discount.ToString("f2"),
                    price = a.Car.PartCars.Sum(s => s.Part.Price).ToString("f2"),
                    priceWithDiscount = ((a.Car.PartCars.Sum(s => s.Part.Price)) * (100 - a.Discount) / 100).ToString("f2")
                }
                ).ToList();
            var rezult = JsonConvert.SerializeObject(sales, Formatting.Indented);
            return rezult;
        }
        private static void InitializeMapper()
        {
            var mapperConfiguration = new MapperConfiguration(config =>
                {
                    config.AddProfile<CarDealerProfile>();
                });
            mapper = new Mapper(mapperConfiguration);
        }
    }
}
