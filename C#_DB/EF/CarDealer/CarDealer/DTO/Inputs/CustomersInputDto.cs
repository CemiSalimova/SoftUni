using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.DTO.Inputs
{
  public  class CustomersInputDto
    {
       
        public string Name { get; set; }

        [JsonProperty("birthDate")]
        public string BirthDate { get; set; }

        
        public Boolean IsYoungDriver { get; set; }
    }
}
