using AutoMapper;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.Common;
using System.Collections.Generic;
using System.Linq;

namespace OMM.Services.Data.DTOs.Assignments
{
    public class AssignmentEditDto : IMapFrom<Assignment>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public int Priority { get; set; }

        public bool IsProjectRelated { get; set; }

        public string StartingDate { get; set; }

        public string Deadline { get; set; }

        public double Progress { get; set; }

        public int StatusId { get; set; }

        public string AssignorName { get; set; }

        public string ExecutorId { get; set; }
        
        public string ProjectId { get; set; }

        public List<string> AssistantsIds { get; set; } = new List<string>();

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Assignment, AssignmentEditDto>()
                .ForMember(destination => destination.StartingDate,
                opts => opts.MapFrom(origin => origin.StartingDate.ToString(Constants.DATETIME_FORMAT)))
                .ForMember(destination => destination.Deadline,
                opts => opts.MapFrom(origin => origin.Deadline == null ? "" : origin.Deadline.Value.ToString(Constants.DATETIME_FORMAT)))
                .ForMember(destination => destination.AssignorName,
                opts => opts.MapFrom(origin => origin.Assignor.FullName))
                .ForMember(destination => destination.AssistantsIds,
                opts => opts.MapFrom(origin => origin.AssignmentsAssistants.Select(aa => aa.AssistantId).ToList()));
        }
    }
}
