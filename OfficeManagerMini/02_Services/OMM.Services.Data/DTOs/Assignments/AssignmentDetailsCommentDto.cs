using AutoMapper;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.Common;

namespace OMM.Services.Data.DTOs.Assignments
{
    public class AssignmentDetailsCommentDto : IMapFrom<Comment>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public string CreatedOn { get; set; }

        public string ModifiedOn { get; set; }

        public string AssignmentId { get; set; }

        public string CommentatorId { get; set; }

        public string CommentatorFullName { get; set; }

        public string CommentatorDepartmentName { get; set; }
        

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Comment, AssignmentDetailsCommentDto>()
                .ForMember(destination => destination.CreatedOn,
                opts => opts.MapFrom(origin => origin.CreatedOn.ToString(Constants.DATETIME_COMMENT_FORMAT)))
                .ForMember(destination => destination.ModifiedOn,
                opts => opts.MapFrom(origin => origin.ModifiedOn == null ? "" : origin.ModifiedOn.Value.ToString(Constants.DATETIME_COMMENT_FORMAT)));
        }
    }
}
