using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.DTO.Outputs.CarsWithParts
{
    public class BigClassPartsCarOutputDto
    {
        public List<CarsForOutputDto> Cars { get; set; }
        public List<PartsForOutputDto> Parts { get; set; }
       
    }
}
