using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OMM.App.Models.InputModels;
using OMM.App.Models.ViewModels;
using OMM.Services.Data;

namespace OMM.App.Controllers
{
    public class AssignmentsController : Controller
    {
        private readonly IEmployeesService employeesService;

        public AssignmentsController(IEmployeesService employeesService)
        {
            this.employeesService = employeesService;
        }

        public async Task<IActionResult> Create(string executorId)
        {
            var currentEmployeeId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            this.ViewData["ExecutorId"] = executorId;
            this.ViewData["AssignorId"] = currentEmployeeId;
            this.ViewData["AssignorName"] = await this.employeesService.GetEmployeeFullNameByIdAsync(currentEmployeeId);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AssignmentCreateInputModel input)
        {
            if (!ModelState.IsValid)
            {
                var currentEmployeeId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                this.ViewData["AssignorId"] = currentEmployeeId;
                this.ViewData["AssignorName"] = await this.employeesService.GetEmployeeFullNameByIdAsync(currentEmployeeId);

                return View();
            }

            return View();
        }
    }
}