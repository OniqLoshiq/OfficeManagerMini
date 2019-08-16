using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMM.App.Common;
using OMM.App.Models.ViewModels;
using OMM.Services.AutoMapper;
using OMM.Services.Data;

namespace OMM.App.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectsService projectsService;

        public ProjectsController(IProjectsService projectsService)
        {
            this.projectsService = projectsService;
        }

        public async Task<IActionResult> MyProjects()
        {
            var myProjects = new ProjectsAlMylViewModel();
            var currentEmployeeId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            myProjects.AllMyOngoingProjects = await this.projectsService.GetMyProjects(currentEmployeeId)
               .Where(p => p.StatusName != Constants.STATUS_COMPLETED)
               .OrderByDescending(p => p.StatusName == Constants.STATUS_INPROGRESS)
               .ThenByDescending(p => p.Priority)
               .To<ProjectsAllMyOngoingViewModel>()
               .ToListAsync();

            myProjects.AllMyCompletedProjects = await this.projectsService.GetMyProjects(currentEmployeeId)
                .Where(p => p.StatusName == Constants.STATUS_COMPLETED)
                .OrderByDescending(p => p.EndDate)
                .ThenByDescending(p => p.Priority)
                .To<ProjectsAllMyCompletedViewModel>()
                .ToListAsync();

            return this.View(myProjects);
        }

        public IActionResult Details()
        {
            return View();
        }


    }
}