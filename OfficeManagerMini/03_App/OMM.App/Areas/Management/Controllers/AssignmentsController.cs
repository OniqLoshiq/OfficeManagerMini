using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OMM.App.Areas.Management.Controllers
{
    [Authorize(Roles = "Admin, Management")]
    public class AssignmentsController : BaseController
    {
        public IActionResult All()
        {
            return View();
        }
    }
}