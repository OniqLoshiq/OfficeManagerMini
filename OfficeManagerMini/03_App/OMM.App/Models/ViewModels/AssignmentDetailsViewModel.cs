using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Assignments;
using System.Collections.Generic;

namespace OMM.App.Models.ViewModels
{
    public class AssignmentDetailsViewModel : IMapFrom<AssignmentDetailsDto>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public int Priority { get; set; }

        public string ProjectName { get; set; }

        public string StartingDate { get; set; }

        public string AssignorFullName { get; set; }

        public string AssignorEmail { get; set; }

        public string ExecutorFullName { get; set; }

        public string ExecutorProfilePicture { get; set; }

        public string ExecutorEmail { get; set; }

        public string ExecutorPhone { get; set; }

        public AssignmentDetailsChangeViewModel ChangeData { get; set; }

        public List<AssignmentDetailsAssistantViewModel> Assistants { get; set; } = new List<AssignmentDetailsAssistantViewModel>();

        public List<AssignmentDetailsCommentViewModel> Comments { get; set; } = new List<AssignmentDetailsCommentViewModel>();
    }
}
