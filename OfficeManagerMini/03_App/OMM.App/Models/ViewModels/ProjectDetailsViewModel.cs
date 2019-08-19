using AutoMapper;
using OMM.App.Common;
using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Projects;
using System.Collections.Generic;
using System.Linq;

namespace OMM.App.Models.ViewModels
{
    public class ProjectDetailsViewModel : IMapFrom<ProjectDetailsDto>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        
        public string Client { get; set; }

        public int Priority { get; set; }

        public string CreatedOn { get; set; }

        public string ReportId { get; set; }

        public ProjectDetailsChangeViewModel ChangeData { get; set; }

        public List<ProjectDetailsParticipantViewModel> Participants { get; set; } = new List<ProjectDetailsParticipantViewModel>();

        public List<AssignmentOngoingListViewModel> AssignmentsOnGoing { get; set; } = new List<AssignmentOngoingListViewModel>();

        public List<AssignmentCompletedListViewModel> AssignmentsCompleted { get; set; } = new List<AssignmentCompletedListViewModel>();


        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ProjectDetailsDto, ProjectDetailsViewModel>()
                .ForMember(destination => destination.AssignmentsOnGoing,
                opts => opts.MapFrom(origin => origin.Assignments
                .Where(a => a.StatusName != Constants.STATUS_COMPLETED)
                .OrderByDescending(a => a.StatusName == Constants.STATUS_INPROGRESS)
                .ThenByDescending(a => a.Priority)))
                .ForMember(destination => destination.AssignmentsCompleted,
                opts => opts.MapFrom(origin => origin.Assignments
                .Where(a => a.StatusName == Constants.STATUS_COMPLETED)
                .OrderByDescending(a => a.EndDate)
                .ThenByDescending(a => a.Priority)));
        }
    }
}
