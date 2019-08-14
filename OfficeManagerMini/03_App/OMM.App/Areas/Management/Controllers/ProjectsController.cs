using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OMM.App.Areas.Management.Models.InputModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMM.App.Areas.Management.Controllers
{
    [Authorize(Roles = "Admin, Management")]
    public class ProjectsController : BaseController
    {
        public IActionResult Create()
        {
            var vm = new ProjectCreateInputModel();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(ProjectCreateInputModel input)
        {
            if(!ModelState.IsValid)
            {
                return this.View(input);
            }

            return Redirect("/");
        }

        public IActionResult GetEmployeesDepartmentViewComponent()
        {
            return ViewComponent("EmployeesDepartmentList", new { employeeId = ""});
        }

        public IActionResult GetProjectPositionsViewComponent()
        {
            return ViewComponent("ProjectPositionsList", new { projectPositionId = 0 });
        }
    }
}