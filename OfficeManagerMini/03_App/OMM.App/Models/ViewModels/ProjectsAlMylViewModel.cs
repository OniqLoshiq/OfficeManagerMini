using System.Collections.Generic;

namespace OMM.App.Models.ViewModels
{
    public class ProjectsAlMylViewModel
    {
        public List<ProjectsAllMyOngoingViewModel> AllMyOngoingProjects { get; set; } = new List<ProjectsAllMyOngoingViewModel>();

        public List<ProjectsAllMyCompletedViewModel> AllMyCompletedProjects { get; set; } = new List<ProjectsAllMyCompletedViewModel>();
    }
}
