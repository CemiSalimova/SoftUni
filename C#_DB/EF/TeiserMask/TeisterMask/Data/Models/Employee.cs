using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeisterMask.Data.Models
{
    public class Employee
    {
        public Employee()
        {
            this.EmployeesTasks = new HashSet<EmployeeTask>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Username  { get; set; }
        [Required]
        [StringLength(255)]
        public string Email { get; set; }
        [Required]
        [StringLength(12)]
        public string Phone { get; set; }
        //nb!
        public ICollection<EmployeeTask> EmployeesTasks  { get; set; }
    }
}
