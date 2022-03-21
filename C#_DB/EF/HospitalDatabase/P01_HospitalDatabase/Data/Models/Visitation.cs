using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P01_HospitalDatabase.Data.Models
{
   public class Visitation
    {
        
        [Key]
        public int VisitationId { get; set; }

        [Required]
        public DateTime Date { get; set; }

       [StringLength(250)]
        public string Comments  { get; set; }

        //NB!
        [ForeignKey("Patient")]
        public int PatientId { get; set; }

        public virtual Patient Patient { get; set; }
       
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public virtual Doctor Doctor{ get; set; }

    }
}
