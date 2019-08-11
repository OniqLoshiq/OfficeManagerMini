using System;

namespace OMM.Services.Data.DTOs.Comments
{
    public class CommentEditDto
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
