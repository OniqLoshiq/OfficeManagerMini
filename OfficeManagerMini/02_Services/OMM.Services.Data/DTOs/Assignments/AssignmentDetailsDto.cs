using AutoMapper;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.Common;
using System.Collections.Generic;

namespace OMM.Services.Data.DTOs.Assignments
{
    public class AssignmentDetailsDto : IMapFrom<Assignment>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public int Priority { get; set; }

        public string ProjectName { get; set; }

        public string StartingDate { get; set; }

        public string AssignorFullName { get; set; }

        public string ExecutorFullName { get; set; }

        public string ExecutorProfilePicture { get; set; }

        public string ExecutorEmail { get; set; }

        public string ExecutorPhone { get; set; }

        public AssignmentDetailsChangeDto ChangeData { get; set; }

        public List<AssignmentDetailsAssistantDto> Assistants { get; set; } = new List<AssignmentDetailsAssistantDto>();

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Assignment, AssignmentDetailsDto>()
                .ForMember(destination => destination.ProjectName,
                opts => opts.MapFrom(origin => origin.IsProjectRelated == true ? origin.Project.Name : "-"))
                .ForMember(destination => destination.StartingDate,
                opts => opts.MapFrom(origin => origin.StartingDate.ToString(Constants.DATETIME_FORMAT)))
                .ForMember(destination => destination.AssignorFullName,
                opts => opts.MapFrom(origin => origin.Assignor.FullName))
                .ForMember(destination => destination.ExecutorFullName,
                opts => opts.MapFrom(origin => origin.Executor.FullName))
                .ForMember(destination => destination.ExecutorProfilePicture,
                opts => opts.MapFrom(origin => origin.Executor.ProfilePicture))
                .ForMember(destination => destination.ExecutorEmail,
                opts => opts.MapFrom(origin => origin.Executor.Email))
                .ForMember(destination => destination.ExecutorPhone,
                opts => opts.MapFrom(origin => origin.Executor.PhoneNumber ?? origin.Executor.PersonalPhoneNumber));
        }
    }
}
