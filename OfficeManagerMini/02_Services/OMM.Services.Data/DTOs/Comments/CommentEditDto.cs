using OMM.Domain;
using OMM.Services.AutoMapper;
using System;

namespace OMM.Services.Data.DTOs.Comments
{
    public class CommentEditDto : IMapFrom<Comment>
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
