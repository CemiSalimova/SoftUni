namespace TeisterMask.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Newtonsoft.Json;
    using TeisterMask.Data.Models;
    using TeisterMask.DataProcessor.ExportDto;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {

        //public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        //{
        //    throw new NotImplementedException();
        //}

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            //var employees = context.Employees
            //    .Where(q => q.EmployeesTasks.Any(s => s.Task.OpenDate >= date))
            //    .ToList()
            //   .Select(w => new
            //   {
            //       w.Username,
            //       Tasks = w.EmployeesTasks
            //       .Where(d=> d.Task.OpenDate >= date)
            //       .OrderByDescending(d => d.Task.DueDate)
            //       .ThenBy(d => d.Task.Name)
            //       .Select(d => new
            //       {
            //           TaskName = d.Task.Name,
            //           OpenDate = d.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture),
            //           DueDate = d.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
            //           LabelType = d.Task.LabelType.ToString(),
            //           ExecutionType = d.Task.ExecutionType.ToString()

            //       })
            //       .ToList()
            //   }).ToList()
            //   .OrderByDescending(w => w.Tasks.Count)
            //   .ThenBy(r => r.Username)
            //   .Take(10)
            //   .ToList();

            //var newEmployees = JsonConvert.SerializeObject(employees,Formatting.Indented);
            //return newEmployees;
            var employees = context.Employees
            .ProjectTo<ExportEmployeeDto>();
            var mappedEmployees = Mapper.Map<List<ExportEmployeeDto>>(employees)
          .OrderByDescending(a => a.Tasks.Count)
          .ThenBy(a => a.Username)
          .Take(10).ToList();

            return JsonConvert.SerializeObject(mappedEmployees, Formatting.Indented);

        }
    }
}