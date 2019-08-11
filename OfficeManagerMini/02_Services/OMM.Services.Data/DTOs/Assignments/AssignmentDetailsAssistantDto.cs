using AutoMapper;
using OMM.Domain;
using OMM.Services.AutoMapper;

namespace OMM.Services.Data.DTOs.Assignments
{
    public class AssignmentDetailsAssistantDto : IMapFrom<AssignmentsEmployees>, IHaveCustomMappings
    {
        public string ProfilePicture { get; set; }

        public string FullName { get; set; }

        public string DepartmentName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<AssignmentsEmployees, AssignmentDetailsAssistantDto>()
                .ForMember(destination => destination.ProfilePicture,
                opts => opts.MapFrom(origin => origin.Assistant.ProfilePicture))
                .ForMember(destination => destination.FullName,
                opts => opts.MapFrom(origin => origin.Assistant.FullName))
                .ForMember(destination => destination.DepartmentName,
                opts => opts.MapFrom(origin => origin.Assistant.Department.Name));
        }
    }
}
