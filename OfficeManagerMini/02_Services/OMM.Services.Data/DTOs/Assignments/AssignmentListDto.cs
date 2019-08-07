using AutoMapper;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.Common;

namespace OMM.Services.Data.DTOs.Assignments
{
    public class AssignmentListDto : IMapFrom<Assignment>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public int Priority { get; set; }

        public bool IsProjectRelated { get; set; }

        public string StartingDate { get; set; }

        public string EndDate { get; set; }

        public string Deadline { get; set; }

        public double Progress { get; set; }

        public string StatusName { get; set; }

        public string AssignorName { get; set; }

        public string ExecutorName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Assignment, AssignmentListDto>()
                .ForMember(destination => destination.StartingDate,
                opts => opts.MapFrom(origin => origin.StartingDate.ToString(Constants.DATETIME_FORMAT)))
                .ForMember(destination => destination.Deadline,
                opts => opts.MapFrom(origin => origin.Deadline != null ? origin.Deadline.Value.ToString(Constants.DATETIME_FORMAT) : "-"))
                .ForMember(destination => destination.EndDate,
                opts => opts.MapFrom(origin => origin.EndDate != null ? origin.EndDate.Value.ToString(Constants.DATETIME_FORMAT) : "-"))
                .ForMember(destination => destination.AssignorName,
                opts => opts.MapFrom(origin => origin.Assignor.FullName))
                .ForMember(destination => destination.ExecutorName,
                opts => opts.MapFrom(origin => origin.Executor.FullName));
        }
    }
}
