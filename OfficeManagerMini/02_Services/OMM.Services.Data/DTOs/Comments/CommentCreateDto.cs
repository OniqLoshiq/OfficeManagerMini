using OMM.Domain;
using OMM.Services.AutoMapper;
using System;

namespace OMM.Services.Data.DTOs.Comments
{
    public class CommentCreateDto : IMapTo<Comment>
    {
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AssignmentId { get; set; }

        public string CommentatorId { get; set; }
    }
}
