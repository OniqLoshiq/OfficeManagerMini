using System.Collections.Generic;

namespace OMM.App.Models.ViewModels
{
    public class AssignmentsListViewModel
    {
        public List<AssignmentOngoingListViewModel> OngoingAssignments { get; set; } = new List<AssignmentOngoingListViewModel>();

        public List<AssignmentCompletedListViewModel> CompletedAssignments { get; set; } = new List<AssignmentCompletedListViewModel>();
    }
}
