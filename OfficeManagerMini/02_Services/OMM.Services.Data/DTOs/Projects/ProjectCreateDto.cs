using AutoMapper;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.Common;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace OMM.Services.Data.DTOs.Projects
{
    public class ProjectCreateDto : IMapTo<Project>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Client { get; set; }

        public int Priority { get; set; }

        public string CreatedOn { get; set; }

        public string Deadline { get; set; }

        public double Progress { get; set; }

        public int StatusId { get; set; }

        public List<ProjectParticipantDto> Participants { get; set; } = new List<ProjectParticipantDto>();

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ProjectCreateDto, Project>()
                .ForMember(destination => destination.CreatedOn,
                opts => opts.MapFrom(origin => DateTime.ParseExact(origin.CreatedOn, Constants.DATETIME_FORMAT, CultureInfo.InvariantCulture)))
                .ForMember(destination => destination.Deadline,
                opts => opts.MapFrom(origin => DateTime.ParseExact(origin.Deadline, Constants.DATETIME_FORMAT, CultureInfo.InvariantCulture)))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); ;
        }
    }
}
