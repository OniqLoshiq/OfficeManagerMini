﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OMM.App.Areas.Management.Controllers
{
    public class EmployeesController : BaseController
    {
        public IActionResult All()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Release()
        {
            return View();
        }
    }
}