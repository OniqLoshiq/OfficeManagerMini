using AutoMapper;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.Common;
using System.Collections.Generic;

namespace OMM.Services.Data.DTOs.Projects
{
    public class ProjectEditDto : IMapFrom<Project>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Client { get; set; }

        public int Priority { get; set; }

        public string CreatedOn { get; set; }

        public string Deadline { get; set; }

        public double Progress { get; set; }

        public int StatusId { get; set; }

        public List<ProjectEditParticipantDto> Participants { get; set; } = new List<ProjectEditParticipantDto>();

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Project, ProjectEditDto>()
                .ForMember(destination => destination.CreatedOn,
                opts => opts.MapFrom(origin => origin.CreatedOn.ToString(Constants.DATETIME_FORMAT)))
                .ForMember(destination => destination.Deadline,
                opts => opts.MapFrom(origin => origin.Deadline == null ? "" : origin.Deadline.Value.ToString(Constants.DATETIME_FORMAT)));
        }
    }
}
