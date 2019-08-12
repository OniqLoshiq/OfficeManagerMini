using Microsoft.AspNetCore.Mvc.Rendering;

namespace OMM.App.Infrastructure.ViewComponents.Models.Projects
{
    public class ProjectsListViewComponentViewModel
    {
        public string ProjectId { get; set; }

        public SelectList Projects { get; set; }
    }
}
