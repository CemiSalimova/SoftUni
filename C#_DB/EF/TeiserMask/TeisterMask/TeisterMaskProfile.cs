namespace TeisterMask
{
    using AutoMapper;
    using System.Globalization;
    using TeisterMask.Data.Models;
    using TeisterMask.DataProcessor.ExportDto;

    public class TeisterMaskProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE OR RENAME THIS CLASS
        public TeisterMaskProfile()
        {
            CreateMap<Employee, ExportEmployeeDto>();
            CreateMap<Employee, ExportEmployeeTaskDto>();
            CreateMap<EmployeeTask, ExportEmployeeTaskDto>()
                .ForMember(a => a.TaskName, b => b.MapFrom(c => c.Task.Name))
                .ForMember(a => a.OpenDate, b => b.MapFrom(c => c.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture)))
                .ForMember(a => a.DueDate, b => b.MapFrom(c => c.Task.DueDate.ToString("d", CultureInfo.InvariantCulture)))
                .ForMember(a => a.LabelType, b => b.MapFrom(c => c.Task.LabelType.ToString()))
               .ForMember(a => a.ExecutionType, b => b.MapFrom(c => c.Task.ExecutionType.ToString()));
            CreateMap<Employee, ExportEmployeeDto>()
               .ForMember(ecp => ecp.Username, opt => opt.MapFrom(c => c.Username))
               .ForMember(ecp => ecp.Tasks,opt => opt.MapFrom(c => c.EmployeesTasks));
        }
    }
}
