using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Theatre.Data.Models
{
   public class Ticket
    {
        //        •	Id – integer, Primary Key
        //•	Price – decimal in the range[1.00….100.00] (required)
        //•	RowNumber – sbyte in range[1….10] (required)
        //•	PlayId – integer, foreign key(required)
        //•	TheatreId – integer, foreign key(required)
       [Key]
        public int Id { get; set; }

        [Required]
        [Range(typeof(decimal), "0.00", "79228162514264337593543950335")]
        //nb!
        public decimal Price { get; set; }

        [Required]
        [Range(typeof(sbyte), "0", "10")]
        public sbyte RowNumber { get; set; }

        [Required]
        [ForeignKey(nameof(Play))]
        public int PlayId { get; set; }
        [Required]
        public Play Play { get; set; }

        [Required]
        [ForeignKey(nameof(Theatre))]
        public int TheatreId { get; set; }
        [Required]
        public Theatre Theatre { get; set; }

    }
}
