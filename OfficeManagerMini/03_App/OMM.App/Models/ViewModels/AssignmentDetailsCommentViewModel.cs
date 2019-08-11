using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Assignments;

namespace OMM.App.Models.ViewModels
{
    public class AssignmentDetailsCommentViewModel : IMapFrom<AssignmentDetailsCommentDto>
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public string CreatedOn { get; set; }

        public string ModifiedOn { get; set; }

        public string AssignmentId { get; set; }

        public string CommentatorId { get; set; }

        public string CommentatorFullName { get; set; }

        public string CommentatorDepartmentName { get; set; }
    }
}
