using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMM.Domain
{
    public class Project
    {
        public Project()
        {
            this.Participants = new HashSet<EmployeesProjectsPositions>();
            this.Assignments = new HashSet<Assignment>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Client { get; set; }

        public int Priority { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? Deadline { get; set; }

        public double Progress { get; set; }

        [ForeignKey(nameof(Status))]
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }

        public virtual Report Report { get; set; }

        public virtual ICollection<EmployeesProjectsPositions> Participants { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }
    }
}
