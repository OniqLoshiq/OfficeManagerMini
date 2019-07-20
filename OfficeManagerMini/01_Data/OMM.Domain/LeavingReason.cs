using System.Collections.Generic;

namespace OMM.Domain
{
    public class LeavingReason
    {
        public LeavingReason()
        {
            this.Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }

        public string Reason { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

        //Retired, Fired, Resigned
    }
}
