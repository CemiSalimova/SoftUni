using System;
using System.Collections.Generic;

namespace P01_StudentSystem.Data.Models
{
    public partial class Course
    {
        public Course()
        {
            HomeworkSubmissions = new HashSet<Homework>();
            Resources = new HashSet<Resource>();
            StudentsEnrolled = new HashSet<StudentCourse>();
        }

        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<Homework> HomeworkSubmissions { get; set; }
        public virtual ICollection<Resource> Resources { get; set; }
        public virtual ICollection<StudentCourse> StudentsEnrolled { get; set; }
    }
}
