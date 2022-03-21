namespace TeisterMask.DataProcessor
{
    using System;
    using System.Collections.Generic;

    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using TeisterMask.Data.Models;
    using TeisterMask.Data.Models.Enums;
    using TeisterMask.DataProcessor.ImportDto;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            XmlRootAttribute xmlRoot = new XmlRootAttribute("Projects");
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportProjectDto[]), xmlRoot);
            using StringReader sr = new StringReader(xmlString);
            ImportProjectDto[] projectsDtos = (ImportProjectDto[])xmlSerializer.Deserialize(sr);
            HashSet<Project> projects = new HashSet<Project>();
            foreach (ImportProjectDto projectDto in projectsDtos)
            {
                if (!IsValid(projectDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                bool isOpenDateValid = DateTime.TryParseExact(projectDto.OpenDate, "dd/MM/yyyy"
                    , CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime openDate);
                if (!isOpenDateValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                DateTime? dueDate = null;
                if (!String.IsNullOrWhiteSpace(projectDto.DueDate))
                {
                    bool isDueDateValid = DateTime.TryParseExact(projectDto.DueDate, "dd/MM/yyyy"
                   , CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dueDateValue);
                    if (!isDueDateValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    dueDate = dueDateValue;
                }
                Project p = new Project
                {
                    Name = projectDto.Name,
                    OpenDate = openDate,
                    DueDate = dueDate
                };
                HashSet<Task> projectTasks = new HashSet<Task>();
                foreach (ImportProjectTaskDto taskDto in projectDto.Tasks)
                {
                    if (!IsValid(taskDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    bool isTaskOpenDateValid = DateTime.TryParseExact(taskDto.TaskOpenDate, "dd/MM/yyyy"
                    , CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime taskOpenDate);
                    if (!isTaskOpenDateValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    bool isTaskDueDateValid = DateTime.TryParseExact(taskDto.TaskDueDate, "dd/MM/yyyy"
                   , CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime taskDueDate);
                    if (!isTaskDueDateValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    if (taskOpenDate<openDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    if (dueDate.HasValue&&taskDueDate>dueDate.Value)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    Task t = new Task
                    {
                        Name = taskDto.Name,
                        OpenDate = taskOpenDate,
                        DueDate = taskDueDate,
                        ExecutionType = (ExecutionType)taskDto.ExecutionType,
                        LabelType = (LabelType)taskDto.LabelType
                    };
                    projectTasks.Add(t);
                }
                p.Tasks = projectTasks;
                projects.Add(p);
                sb.AppendLine(String.Format(SuccessfullyImportedProject, p.Name, p.Tasks.Count));
            }
            context.Projects.AddRange(projects);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            var employeesDto = JsonConvert.DeserializeObject<IEnumerable<ImportEmployeeDto>>(jsonString);
            HashSet<Employee> employees = new HashSet<Employee>();
            foreach (ImportEmployeeDto emplDto in employeesDto)
            {
                if (!IsValid(emplDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Employee employee = new Employee
                {
                    Username = emplDto.Username,
                    Email = emplDto.Email,
                    Phone = emplDto.Phone
                };
               
                foreach (var taskId in emplDto.Tasks.Distinct())
                {
                    Task t = context.Tasks.Find(taskId);
                    if (t==null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    employee.EmployeesTasks.Add(new EmployeeTask    
                    {
                        Task = t
                    });
                }
                employees.Add(employee);
                sb.AppendLine(String.Format(SuccessfullyImportedEmployee, employee.Username, employee.EmployeesTasks.Count));
            }
            context.Employees.AddRange(employees);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}