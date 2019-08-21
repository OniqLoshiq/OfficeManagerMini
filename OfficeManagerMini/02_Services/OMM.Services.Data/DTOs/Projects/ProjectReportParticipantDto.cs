using AutoMapper;
using OMM.Domain;
using OMM.Services.AutoMapper;

namespace OMM.Services.Data.DTOs.Projects
{
    public class ProjectReportParticipantDto : IMapFrom<EmployeesProjectsPositions>, IHaveCustomMappings
    {
        public string ParticipantId { get; set; }

        public string ProfilePicture { get; set; }

        public string ParticipantFullName { get; set; }

        public string DepartmentName { get; set; }

        public string ProjectPositionName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<EmployeesProjectsPositions, ProjectReportParticipantDto>()
                .ForMember(destination => destination.ParticipantId,
                opts => opts.MapFrom(origin => origin.EmployeeId))
                .ForMember(destination => destination.ProfilePicture,
                opts => opts.MapFrom(origin => origin.Employee.ProfilePicture))
                .ForMember(destination => destination.ParticipantFullName,
                opts => opts.MapFrom(origin => origin.Employee.FullName))
                .ForMember(destination => destination.DepartmentName,
                opts => opts.MapFrom(origin => origin.Employee.Department.Name));
        }
    }
}
