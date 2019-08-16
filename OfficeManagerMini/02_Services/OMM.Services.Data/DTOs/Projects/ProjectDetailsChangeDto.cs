using AutoMapper;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.Common;

namespace OMM.Services.Data.DTOs.Projects
{
    public class ProjectDetailsChangeDto : IMapFrom<Project>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string EndDate { get; set; }

        public string Deadline { get; set; }

        public double Progress { get; set; }

        public int StatusId { get; set; }

        public string StatusName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Project, ProjectDetailsChangeDto>()
               .ForMember(destination => destination.EndDate,
               opts => opts.MapFrom(origin => origin.EndDate == null ? "-" : origin.EndDate.Value.ToString(Constants.DATETIME_FORMAT)))
               .ForMember(destination => destination.Deadline,
               opts => opts.MapFrom(origin => origin.Deadline == null ? "-" : origin.Deadline.Value.ToString(Constants.DATETIME_FORMAT)));
        }
    }
}
