using CarDealer.DTO.Inputs;
using CarDealer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.DTO
{
    public class CarsInputDto
    {
        [JsonProperty("make",Order=0)]
        public string Make { get; set; }
        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("travelledDistance")]
        public long TravelledDistance { get; set; }

       // [JsonProperty("partsId")]
        public ICollection<int> PartsId { get; set; }
    }
}
