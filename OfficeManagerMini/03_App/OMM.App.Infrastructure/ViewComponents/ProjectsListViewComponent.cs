using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OMM.App.Infrastructure.ViewComponents.Models.Projects;
using OMM.Services.AutoMapper;
using OMM.Services.Data;

namespace OMM.App.Infrastructure.ViewComponents
{
    public class ProjectsListViewComponent : ViewComponent
    {
        private readonly IProjectsService projectsService;

        public ProjectsListViewComponent(IProjectsService projectsService)
        {
            this.projectsService = projectsService;
        }

        public IViewComponentResult Invoke(string projectId)
        {
            var vm = new ProjectsListViewComponentViewModel();

            vm.ProjectId = projectId;

            vm.Projects = new SelectList(this.projectsService.GetAllProjectsForList().To<ProjectListViewModel>(), "Id", "Name");

            return View(vm);
        }
    }
}
