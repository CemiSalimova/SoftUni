using System;
using System.Collections.Generic;

namespace P01_StudentSystem.Data.Models
{
    public partial class Student
    {
        public Student()
        {
            HomeworkSubmissions = new HashSet<Homework>();
            CourseEnrollments = new HashSet<StudentCourse>();
        }

        public int StudentId { get; set; }
        public DateTime? Birthday { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegisteredOn { get; set; }

        public virtual ICollection<Homework> HomeworkSubmissions { get; set; }
        public virtual ICollection<StudentCourse> CourseEnrollments { get; set; }
    }
}
