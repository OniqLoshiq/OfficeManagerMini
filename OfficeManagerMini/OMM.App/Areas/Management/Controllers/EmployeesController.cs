using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OMM.App.Areas.Management.Controllers
{
    [Area("Management")]
    public class EmployeesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}