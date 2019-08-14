using Microsoft.AspNetCore.Mvc.Rendering;

namespace OMM.App.Infrastructure.ViewComponents.Models.ProjectPositions
{
    public class ProjectPositionViewComponentViewModel
    {
        public int ProjectPositionId { get; set; }

        public SelectList ProjectPositions { get; set; }
    }
}
