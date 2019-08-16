using AutoMapper;
using OMM.Domain;
using OMM.Services.AutoMapper;

namespace OMM.Services.Data.DTOs.Projects
{
    public class ProjectDetailsParticipantDto : IMapFrom<EmployeesProjectsPositions>, IHaveCustomMappings
    {
        public string ParticipantId { get; set; }

        public string ProfilePicture { get; set; }

        public string FullName { get; set; }

        public int ProjectPositionId { get; set; }

        public string ProjectPositionName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<EmployeesProjectsPositions, ProjectDetailsParticipantDto>()
                .ForMember(destination => destination.ParticipantId,
                opts => opts.MapFrom(origin => origin.EmployeeId))
                .ForMember(destination => destination.ProfilePicture,
                opts => opts.MapFrom(origin => origin.Employee.ProfilePicture))
                .ForMember(destination => destination.FullName,
                opts => opts.MapFrom(origin => origin.Employee.FullName));
        }
    }
}
