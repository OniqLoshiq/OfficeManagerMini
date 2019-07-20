namespace OMM.Domain
{
    public class AssignmentsEmployees
    {
        public string AssignmentId { get; set; }
        public virtual Assignment Assignment { get; set; }
        
        public string AssistantId { get; set; }
        public virtual Employee Assistant { get; set; }
    }
}
