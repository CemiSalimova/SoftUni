using P01_StudentSystem.Data;
using System;
using System.Linq;
using System.Text;

namespace P01_StudentSystem
{
   public class StartUp
    {
        static void Main(string[] args)
        {
            var context = new StudentSystemContext();
            Console.WriteLine(GetEmployeesByFirstNameStartingWithSa(context));
        }
        public static string GetEmployeesByFirstNameStartingWithSa(StudentSystemContext context)
        {
            StringBuilder sb = new StringBuilder();

            var students = context.Students
                .Where(x => x.Name.StartsWith("Sa"))
                .Select(x => new
                {
                    Name = x.Name,
                    StudentCourse = x.CourseEnrollments
                    .Select(c => new
                    {
                        CourseName = c.Course.Name,
                        CourseStartDate = c.Course.StartDate,
                        CoursePrice = c.Course.Price,
                        HomeworkSubm = c.Course.HomeworkSubmissions
                    .Select(
                       h => new
                       {
                           HomeworkEndDate = h.EndDate,
                           HomeworkTime = h.SubmissionTime,

                       })
                    }),
                    
                }
                )
                .OrderBy(x => x.Name)

                .ToList();

            foreach (var e in students)
            {
                sb.AppendLine($"{e.Name}");
                foreach (var student in e.StudentCourse)
                {
                    sb.AppendLine($"{student.CourseName} - {student.CourseStartDate} - {student.CoursePrice}");
                    foreach (var c in student.HomeworkSubm)
                    {
                        sb.AppendLine($"{c.HomeworkTime}");
                    }
                }

            }

            return sb.ToString().TrimEnd();
        }
    }
}
