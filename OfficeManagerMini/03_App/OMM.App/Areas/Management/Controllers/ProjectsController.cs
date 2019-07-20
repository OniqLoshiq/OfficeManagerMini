using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OMM.App.Areas.Management.Controllers
{
    public class ProjectsController : BaseController
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}