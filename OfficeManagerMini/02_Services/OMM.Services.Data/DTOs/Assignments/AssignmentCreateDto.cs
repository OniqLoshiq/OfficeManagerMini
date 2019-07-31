using AutoMapper;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.Common;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace OMM.Services.Data.DTOs.Assignments
{
    public class AssignmentCreateDto : IMapTo<Assignment>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public int Priority { get; set; }

        public bool IsProjectRelated { get; set; }

        public string StartingDate { get; set; }

        public string Deadline { get; set; }

        public double Progress { get; set; }

        public int StatusId { get; set; }

        public string AssignorId { get; set; }

        public string ExecutorId { get; set; }

        public string ProjectId { get; set; }

        public List<string> AssistantsIds { get; set; } = new List<string>();

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<AssignmentCreateDto, Assignment>()
                .ForMember(destination => destination.StartingDate,
                opts => opts.MapFrom(origin => DateTime.ParseExact(origin.StartingDate, Constants.DATETIME_FORMAT, CultureInfo.InvariantCulture)))
                .ForMember(destination => destination.Deadline,
                opts => opts.MapFrom(origin => DateTime.ParseExact(origin.Deadline, Constants.DATETIME_FORMAT, CultureInfo.InvariantCulture)))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
