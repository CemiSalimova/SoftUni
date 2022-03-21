using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CarDealer.DTO.Outputs
{
 public  class CustomersOrderOutputDto
    {
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        [JsonProperty("birthDate",PropertyName= "BirthDate")]
        public DateTime BirthDate { get; set; }
        [JsonProperty(PropertyName = "IsYoungDriver")]
        public Boolean IsYoungDriver { get; set; }
    }
}
