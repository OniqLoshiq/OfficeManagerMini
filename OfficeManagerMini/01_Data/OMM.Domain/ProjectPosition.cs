using System.Collections.Generic;

namespace OMM.Domain
{
    public class ProjectPosition
    {
        public ProjectPosition()
        {
            this.EmployeesProjects = new HashSet<EmployeesProjectsPositions>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<EmployeesProjectsPositions> EmployeesProjects { get; set; }

        //Project Manager, Participant, Assistant
    }
}
