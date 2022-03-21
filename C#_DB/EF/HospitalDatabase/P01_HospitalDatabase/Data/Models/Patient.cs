using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P01_HospitalDatabase.Data.Models
{
    public class Patient
    {
        public Patient()
        {
            this.Prescriptions = new HashSet<PatientMedicament>();

            this.Visitations = new HashSet<Visitation>();
            this.Diagnoses = new HashSet<Diagnose>();

        }
        [Key]
        public int PatientId { get; set; }


        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }


        [StringLength(50)]
        public string Address { get; set; }


        [StringLength(80)]
        public string Email { get; set; }

        public Boolean HasInsurance { get; set; }
        public virtual ICollection<PatientMedicament> Prescriptions { get; set; }

        public virtual ICollection<Visitation> Visitations { get; set; }
        public virtual ICollection<Diagnose> Diagnoses { get; set; }
    }
}
