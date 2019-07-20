using System.Collections.Generic;

namespace OMM.Domain
{
    public class Department
    {
        public Department()
        {
            this.Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

        //Management board, Administration, Accounting, Engineering, HR, etc.
    }
}
