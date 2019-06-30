namespace OMM.Domain
{
    public class EmployeesProjectsPositions
    {
        public string EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public string ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public string ProjectPositionId { get; set; }
        public virtual ProjectPosition ProjectPosition { get; set; }
    }
}
