using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P01_HospitalDatabase.Data.Models
{
  public  class Diagnose
    {
        [Key]
        public int DiagnoseId { get; set; }

       [StringLength(50)]
        public string Name { get; set; }
        
       [StringLength(250)]
        public string Comments { get; set; }
        //NB!

        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }
       
    }
}
