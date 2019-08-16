using System.Collections.Generic;

namespace OMM.App.Areas.Management.Models.ViewModels
{
    public class ProjectsAllViewModel
    {
        public List<ProjectsAllOngoingViewModel> AllOngoingProjects { get; set; } = new List<ProjectsAllOngoingViewModel>();

        public List<ProjectsAllCompletedViewModel> AllCompletedProjects { get; set; } = new List<ProjectsAllCompletedViewModel>();
    }
}
