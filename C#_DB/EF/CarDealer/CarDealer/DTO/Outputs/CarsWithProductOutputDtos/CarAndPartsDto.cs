using CarDealer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.DTO.Outputs.CarsWithProductOutputDtos
{
    public class CarAndPartsDto
    {
        [JsonProperty("car")]
        public CarDto Car { get; set; }
        [JsonProperty("parts")]
        public List<PartDto> Parts { get; set; }
    }
}
