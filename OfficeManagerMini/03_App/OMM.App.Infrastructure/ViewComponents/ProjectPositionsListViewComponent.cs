using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OMM.App.Infrastructure.ViewComponents.Models.ProjectPositions;
using OMM.Services.Data;
using System.Linq;

namespace OMM.App.Infrastructure.ViewComponents
{
    public class ProjectPositionsListViewComponent : ViewComponent
    {
        private readonly IProjectPositionsService projectPositionsService;

        public ProjectPositionsListViewComponent(IProjectPositionsService projectPositionsService)
        {
            this.projectPositionsService = projectPositionsService;
        }

        public IViewComponentResult Invoke(int projectPositionId)
        {
            var vm = new ProjectPositionViewComponentViewModel
            {
                ProjectPositionId = projectPositionId
            };

            vm.ProjectPositions = new SelectList(this.projectPositionsService.GetProjectPositions().ToList(), "Id", "Name");

            return View(vm);
        }
    }
}
