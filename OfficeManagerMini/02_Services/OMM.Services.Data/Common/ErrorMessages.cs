namespace OMM.Services.Data.Common
{
    public static class ErrorMessages
    {
        public const string StatusNullParameter = "Status name cannot be null!";

        public const string StatusNameNullReference = "Status with name {0} does not exist!";

        public const string StatusInvalidRange = "Status with id {0} is not found!";

        public const string ProjectPositionInvalidRange = "Project position with id {0} is not found!";

        public const string DepartmentInvalidRange = "Department with id {0} is not found!";

        public const string AssetIdNullReference = "Asset with id {0} does not exist!";

        public const string AssetTypeIdNullReference = "Asset type with id {0} does not exist!";

        public const string AssignmentIdNullReference = "Assignment with id {0} does not exist!";

        public const string EmployeeIdNullReference = "Employee with id {0} does not exist!";

        public const string CommentIdNullReference = "Comment with id {0} does not exist!";

        public const string ReportIdNullReference = "Report with id {0} does not exist!";

        public const string ActivityIdNullReference = "Activity with id {0} does not exist!";

        public const string ProjectIdNullReference = "Project with id {0} does not exist!";

        public const string ReportInvalidProjectId = "There is already a report for project with id {0}!";

        public const string ProjectParticipantNullReference = "Project and/or Participant does not exists!";

        public const string ProjectPositionNullReference = "ProjectPosition with id {0} does not exist!";

        public const string ProjectParticipantsToRemoveNullReference = "Project participants to remove are invalid!";

        public const string AssistantsToAddNullException = "Assistants to add are not found!";

        public const string AssistantArgumentException = "Assistant/s is/are already part of the assignment!";


    }
}
