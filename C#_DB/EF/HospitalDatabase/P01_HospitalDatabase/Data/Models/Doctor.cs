using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P01_HospitalDatabase.Data.Models
{
  public  class Doctor
    {
        public Doctor()
        {
            this.Prescriptions = new HashSet<PatientMedicament>();
            this.Visitations = new HashSet<Visitation>();
        }
        [Key]
        public int DoctorId { get; set; }
        
        [StringLength(100)]
        public string Name { get; set; }
       
        [StringLength(100)]
        public string Specialty { get; set; }
        public virtual ICollection<PatientMedicament> Prescriptions { get; set; }
        public virtual ICollection<Visitation> Visitations{ get; set; }
    }
}
