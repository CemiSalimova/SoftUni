using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarDealer.Models
{
   public class Supplier
    {
        public Supplier()
        {
            this.Parts = new HashSet<Part>();
        }
       
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Boolean IsImporter { get; set; }
        //nb!
        public ICollection<Part> Parts { get; set; }
    }
}
