using System.Collections.Generic;

namespace OMM.App.Areas.Management.Models.ViewModels
{
    public class AssignmentsAllListViewModel
    {
        public List<AssignmentOngoingListViewModel> OngoingAssignments { get; set; } = new List<AssignmentOngoingListViewModel>();

        public List<AssignmentCompletedListViewModel> CompletedAssignments { get; set; } = new List<AssignmentCompletedListViewModel>();
    }
}
