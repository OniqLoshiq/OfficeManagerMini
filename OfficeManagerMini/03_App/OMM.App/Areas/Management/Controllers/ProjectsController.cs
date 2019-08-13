using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMM.App.Areas.Management.Controllers
{
    [Authorize(Roles = "Admin, Management")]
    public class ProjectsController : BaseController
    {
        public IActionResult Create()
        {
            ViewBag.ProjectPositions = new List<SelectListItem>
                                 {
                                     new SelectListItem {Text = "Project manager", Value = "1"},
                                     new SelectListItem {Text = "Participant", Value = "2"},
                                     new SelectListItem {Text = "Assistant", Value = "3"}
                                 };

            return ViewComponent("EmployeesDepartmentList", new { employeeId = "" });

            return View();
        }
    }
}