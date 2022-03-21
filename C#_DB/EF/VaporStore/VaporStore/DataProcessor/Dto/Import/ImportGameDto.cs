using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VaporStore.DataProcessor.Dto.Import
{
   public class ImportGameDto
    {
        //"Price": 0,
        //    "ReleaseDate": "2013-07-09",
        //    "Developer": "Valid Dev",
        //    "Genre": "Valid Genre",
        //    "Tags": ["Valid Tag"]
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(typeof(decimal),"0.0", "79228162514264337593543950335")]
        public decimal Price { get; set; }
       
        public string ReleaseDate  { get; set; }
        
        [Required]
        [JsonProperty("Developer")]
        public string Developer { get; set; }

        [Required]
        public string Genre { get; set; }
        
        [MinLength(1)]
        public string[] Tags { get; set; }
    }
}
