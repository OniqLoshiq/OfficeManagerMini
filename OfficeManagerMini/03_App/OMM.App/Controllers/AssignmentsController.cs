using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OMM.App.Models.InputModels;
using OMM.Services.AutoMapper;
using OMM.Services.Data;
using OMM.Services.Data.DTOs.Assignments;

namespace OMM.App.Controllers
{
    public class AssignmentsController : Controller
    {
        private readonly IEmployeesService employeesService;
        private readonly IAssignmentsService assignmentsService;

        public AssignmentsController(IEmployeesService employeesService, IAssignmentsService assignmentsService)
        {
            this.employeesService = employeesService;
            this.assignmentsService = assignmentsService;
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

            var inputDto = input.To<AssignmentCreateDto>();

            await this.assignmentsService.CreateAssignmentAsync(inputDto);

            return Redirect("/");
        }
    }
}