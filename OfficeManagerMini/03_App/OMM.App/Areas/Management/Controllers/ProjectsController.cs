using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OMM.App.Areas.Management.Controllers
{
    [Authorize(Roles = "Admin, Management")]
    public class ProjectsController : BaseController
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}