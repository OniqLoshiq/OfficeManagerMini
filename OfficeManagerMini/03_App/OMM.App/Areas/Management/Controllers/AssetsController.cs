using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OMM.App.Common;
using OMM.App.Infrastructure.CustomAuthorization;

namespace OMM.App.Areas.Management.Controllers
{
    public class AssetsController : BaseController
    {
        public IActionResult All()
        {
            return View();
        }

        [MinimumAccessLevel(AccessLevelValue.Seven)]
        public IActionResult Create()
        {
            return View();
        }
    }
}