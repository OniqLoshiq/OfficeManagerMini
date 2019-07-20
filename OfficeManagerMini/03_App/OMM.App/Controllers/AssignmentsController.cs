using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OMM.App.Controllers
{
    public class AssignmentsController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}