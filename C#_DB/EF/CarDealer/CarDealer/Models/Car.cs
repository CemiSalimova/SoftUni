using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarDealer.Models
{
   public class Car
    {
        public Car()
        {
            this.PartCars = new HashSet<PartCar>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public long TravelledDistance { get; set; }
        //nb!
        public virtual ICollection<PartCar> PartCars { get; set; }
    }
}
