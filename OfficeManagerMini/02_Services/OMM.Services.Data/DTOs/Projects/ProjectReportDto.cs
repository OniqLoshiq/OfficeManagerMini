using AutoMapper;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.Common;
using System.Collections.Generic;

namespace OMM.Services.Data.DTOs.Projects
{
    public class ProjectReportDto : IMapFrom<Project>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Client { get; set; }

        public int Priority { get; set; }

        public string CreatedOn { get; set; }

        public string Deadline { get; set; }

        public string EndDate { get; set; }
        
        public double Progress { get; set; }

        public string StatusName { get; set; }

        public List<ProjectReportParticipantDto> Participants { get; set; } = new List<ProjectReportParticipantDto>();

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Project, ProjectReportDto>()
                .ForMember(destination => destination.CreatedOn,
                opts => opts.MapFrom(origin => origin.CreatedOn.ToString(Constants.DATETIME_FORMAT)))
                .ForMember(destination => destination.Deadline,
                opts => opts.MapFrom(origin => origin.Deadline == null ? "-" : origin.Deadline.Value.ToString(Constants.DATETIME_FORMAT)))
                .ForMember(destination => destination.EndDate,
                opts => opts.MapFrom(origin => origin.EndDate == null ? "-" : origin.EndDate.Value.ToString(Constants.DATETIME_FORMAT)));
        }
    }
}
