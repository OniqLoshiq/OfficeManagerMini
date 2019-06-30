using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMM.Domain
{
    public class Comment
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [ForeignKey(nameof(Assignment))]
        public string AssignmentId { get; set; }
        public virtual Assignment Assignment { get; set; }

        [ForeignKey(nameof(Commentator))]
        public string CommentatorId { get; set; }
        public virtual Employee Commentator { get; set; }
    }
}
