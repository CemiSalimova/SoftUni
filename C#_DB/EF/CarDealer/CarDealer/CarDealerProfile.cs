using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using CarDealer.DTO;
using CarDealer.DTO.Inputs;
using CarDealer.DTO.Outputs;
using CarDealer.DTO.Outputs.CarsWithProductOutputDtos;
using CarDealer.Models;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            CreateMap<SuppliersInputDto, Supplier>();
            CreateMap<PartsInputDto, Part>();
            CreateMap<CarPartInputDto, Part>();
            CreateMap<CustomersInputDto, Customer>();
            CreateMap<Customer, CustomersOrderOutputDto>();
            CreateMap<Car, CarsFromModelOutputDto>();
            CreateMap<Supplier, LocalSupplierOutputDto>()
                .ForMember(a => a.Count, b => b.MapFrom(c => c.Parts.Count()));
            #region GetCarsWithParts


            CreateMap<Car, CarAndPartsDto>()
                .ForMember(a => a.Car, b => b.MapFrom(c => c))
               .ForMember(a => a.Parts, b => b.MapFrom(c => c.PartCars));

            CreateMap<PartCar, PartDto>()
              .ForMember(a => a.Name, b => b.MapFrom(c => c.Part.Name))
             .ForMember(a => a.Price, b => b.MapFrom(c => $"{c.Part.Price:f2}"));

            #endregion

        }
    }
}
