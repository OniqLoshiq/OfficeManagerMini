using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OMM.App.Areas.Management.Controllers
{
    public class AssetsController : BaseController
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}