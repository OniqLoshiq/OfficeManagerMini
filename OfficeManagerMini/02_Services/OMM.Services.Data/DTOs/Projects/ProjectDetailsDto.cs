using AutoMapper;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.Common;
using System.Collections.Generic;

namespace OMM.Services.Data.DTOs.Projects
{
    public class ProjectDetailsDto : IMapFrom<Project>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Client { get; set; }

        public int Priority { get; set; }

        public string CreatedOn { get; set; }

        public string ReportId { get; set; }
        
        public ProjectDetailsChangeDto ChangeData { get; set; }

        public List<ProjectDetailsParticipantDto> Participants { get; set; } = new List<ProjectDetailsParticipantDto>();


        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Project, ProjectDetailsDto>()
                .ForMember(destination => destination.CreatedOn,
                opts => opts.MapFrom(origin => origin.CreatedOn.ToString(Constants.DATETIME_FORMAT)))
                .ForMember(destination => destination.ChangeData,
                opts => opts.MapFrom(origin => origin))
                .ForMember(destination => destination.Participants,
                opts => opts.MapFrom(origin => origin.Participants));
        }
    }
}
