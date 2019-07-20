using System.Collections.Generic;

namespace OMM.Domain
{
    public class Status
    {
        public Status()
        {
            this.Assignments = new HashSet<Assignment>();
            this.Projects = new HashSet<Project>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

        // In Progress, Frozen, Waiting, Delayed, Completed
    }
}
