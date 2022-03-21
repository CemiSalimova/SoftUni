using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarDealer.Models
{
  public  class PartCar
    {
        [Required]
        [ForeignKey(nameof(Part))]
        public int PartId { get; set; }
        public Part Part { get; set; }

        [Required]
        [ForeignKey(nameof(Car))]
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
