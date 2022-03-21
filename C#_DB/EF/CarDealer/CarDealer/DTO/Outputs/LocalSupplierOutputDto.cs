using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.DTO.Outputs
{
   public class LocalSupplierOutputDto
    {
      
        public int Id { get; set; }
        public String Name { get; set; }
        [JsonProperty(PropertyName="PartsCount")]
        public int Count { get; set; }

    }
}
