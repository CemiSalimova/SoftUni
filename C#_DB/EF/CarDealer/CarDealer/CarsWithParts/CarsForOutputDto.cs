using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.DTO.Outputs.CarsWithParts
{
   public class CarsForOutputDto
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public long TravelledDistance { get; set; }
       public List<PartsForOutputDto> Parts { get; set; }
    }
}
