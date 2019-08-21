using AutoMapper;
using OMM.Domain;
using OMM.Services.AutoMapper;

namespace OMM.Services.Data.DTOs.Projects
{
    public class ProjectEditParticipantDto : IMapFrom<EmployeesProjectsPositions>, IMapTo<EmployeesProjectsPositions>, IHaveCustomMappings
    {
        public string EmployeeId { get; set; }

        public string ProfilePicture { get; set; }

        public string EmployeeFullName { get; set; }

        public string DepartmentName { get; set; }
        
        public string ProjectId { get; set; }

        public int ProjectPositionId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<EmployeesProjectsPositions, ProjectEditParticipantDto>()
                .ForMember(destination => destination.ProfilePicture,
                opts => opts.MapFrom(origin => origin.Employee.ProfilePicture))
                .ForMember(destination => destination.DepartmentName,
                opts => opts.MapFrom(origin => origin.Employee.Department.Name));
        }
    }
}
