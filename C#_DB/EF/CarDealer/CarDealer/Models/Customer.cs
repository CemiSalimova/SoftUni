using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarDealer.Models
{
   public class Customer
    {
        public Customer()
        {
            this.Sales = new HashSet<Sale>();
        }
        
       [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }


        [JsonProperty("birthDate")]
        [Required]
        public DateTime BirthDate { get; set; }
       
        public Boolean IsYoungDriver { get; set; }
        //nb!
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
