using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMM.Domain
{
    public class Assignment
    {
        public Assignment()
        {
            this.AssignmentsAssistants = new HashSet<AssignmentsEmployees>();
            this.Comments = new HashSet<Comment>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public int Priority { get; set; }

        public bool IsProjectRelated { get; set; }

        public DateTime StartingDate { get; set; }

        public DateTime? EndDate { get; set; }
        
        public DateTime? Deadline { get; set; }

        public double Progress { get; set; }

        [ForeignKey(nameof(Status))]
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }

        [ForeignKey(nameof(Assignor))]
        public string AssignorId { get; set; }
        public virtual Employee Assignor { get; set; }

        [ForeignKey(nameof(Executor))]
        public string ExecutorId { get; set; }
        public virtual Employee Executor { get; set; }

        [ForeignKey(nameof(Project))]
        public string ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public virtual ICollection<AssignmentsEmployees> AssignmentsAssistants { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
