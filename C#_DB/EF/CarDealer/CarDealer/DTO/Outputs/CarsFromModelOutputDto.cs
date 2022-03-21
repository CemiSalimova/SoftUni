using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.DTO.Outputs
{
   public class CarsFromModelOutputDto
    {
        [JsonProperty(PropertyName="Id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "Make")]
        public string Make { get; set; }

        [JsonProperty(PropertyName = "Model")]
        public string Model { get; set; }

        [JsonProperty("travelledDistance",PropertyName = "TravelledDistance")]
        public long TravelledDistance { get; set; }
    }
}
