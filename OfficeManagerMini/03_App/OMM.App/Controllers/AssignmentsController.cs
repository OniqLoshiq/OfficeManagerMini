﻿using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMM.App.Common;
using OMM.App.Models.InputModels;
using OMM.App.Models.ViewModels;
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

            var inputModel = new AssignmentCreateInputModel();

            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AssignmentCreateInputModel input)
        {
            if (!ModelState.IsValid)
            {
                var currentEmployeeId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                this.ViewData["AssignorId"] = currentEmployeeId;
                this.ViewData["AssignorName"] = await this.employeesService.GetEmployeeFullNameByIdAsync(currentEmployeeId);

                return View(input);
            }

            var inputDto = input.To<AssignmentCreateDto>();

            await this.assignmentsService.CreateAssignmentAsync(inputDto);

            return RedirectToAction("FromMe");
        }

        public async Task<IActionResult> MyAssignments()
        {
            var assignments = new AssignmentsListViewModel();

            var employeeId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            assignments.OngoingAssignments = await this.assignmentsService.GetAllMyAssignments(employeeId)
               .Where(a => a.StatusName != Constants.STATUS_COMPLETED)
               .OrderByDescending(a => a.StatusName == Constants.STATUS_INPROGRESS)
               .ThenByDescending(a => a.Priority)
               .To<AssignmentOngoingListViewModel>()
               .ToListAsync();

            assignments.CompletedAssignments = await this.assignmentsService.GetAllMyAssignments(employeeId)
                .Where(a => a.StatusName == Constants.STATUS_COMPLETED)
                .OrderByDescending(a => a.EndDate)
                .ThenByDescending(a => a.Priority)
                .To<AssignmentCompletedListViewModel>()
                .ToListAsync();

            return this.View(assignments);
        }

        public async Task<IActionResult> FromMe()
        {
            var assignments = new AssignmentsListViewModel();

            var assignorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            assignments.OngoingAssignments = await this.assignmentsService.GetAllAssignmentsFromMe(assignorId)
               .Where(a => a.StatusName != Constants.STATUS_COMPLETED)
               .OrderByDescending(a => a.StatusName == Constants.STATUS_INPROGRESS)
               .ThenByDescending(a => a.Priority)
               .To<AssignmentOngoingListViewModel>()
               .ToListAsync();

            assignments.CompletedAssignments = await this.assignmentsService.GetAllAssignmentsFromMe(assignorId)
                .Where(a => a.StatusName == Constants.STATUS_COMPLETED)
                .OrderByDescending(a => a.EndDate)
                .ThenByDescending(a => a.Priority)
                .To<AssignmentCompletedListViewModel>()
                .ToListAsync();

            return this.View(assignments);
        }

        public async Task<IActionResult> ForMe()
        {
            var assignments = new AssignmentsListViewModel();

            var executorId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            assignments.OngoingAssignments = await this.assignmentsService.GetAllAssignmentsForMe(executorId)
               .Where(a => a.StatusName != Constants.STATUS_COMPLETED)
               .OrderByDescending(a => a.StatusName == Constants.STATUS_INPROGRESS)
               .ThenByDescending(a => a.Priority)
               .To<AssignmentOngoingListViewModel>()
               .ToListAsync();

            assignments.CompletedAssignments = await this.assignmentsService.GetAllAssignmentsForMe(executorId)
                .Where(a => a.StatusName == Constants.STATUS_COMPLETED)
                .OrderByDescending(a => a.EndDate)
                .ThenByDescending(a => a.Priority)
                .To<AssignmentCompletedListViewModel>()
                .ToListAsync();

            return this.View(assignments);
        }

        public async Task<IActionResult> AsAssistant()
        {
            var assignments = new AssignmentsListViewModel();

            var assistantId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            assignments.OngoingAssignments = await this.assignmentsService.GetAllAssignmentsAsAssistant(assistantId)
               .Where(a => a.StatusName != Constants.STATUS_COMPLETED)
               .OrderByDescending(a => a.StatusName == Constants.STATUS_INPROGRESS)
               .ThenByDescending(a => a.Priority)
               .To<AssignmentOngoingListViewModel>()
               .ToListAsync();

            assignments.CompletedAssignments = await this.assignmentsService.GetAllAssignmentsAsAssistant(assistantId)
                .Where(a => a.StatusName == Constants.STATUS_COMPLETED)
                .OrderByDescending(a => a.EndDate)
                .ThenByDescending(a => a.Priority)
                .To<AssignmentCompletedListViewModel>()
                .ToListAsync();

            return this.View(assignments);
        }

        public async Task<IActionResult> Details(string id)
        {
            var assignment = (await this.assignmentsService.GetAssignmentDetails(id).SingleOrDefaultAsync()).To<AssignmentDetailsViewModel>();

            assignment.Comments = assignment.Comments.OrderByDescending(c => c.CreatedOn).ToList();

            return this.View(assignment);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeData(AssignmentDetailsChangeViewModel input)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("Details", new { id = input.Id });
            }

            var assignmentDataToChange = input.To<AssignmentDetailsChangeDto>();

            await this.assignmentsService.ChangeDataAsync(assignmentDataToChange);

            return RedirectToAction("Details", new { id = input.Id });
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return RedirectToAction("MyAssignments");
            }

            await this.assignmentsService.DeleteAsync(id);

            return RedirectToAction("MyAssignments");
        }

        public async Task<IActionResult> Edit(string id)
        {
            var assignmentToEdit = (await this.assignmentsService.GetAssignmentToEditAsync(id)).To<AssignmentEditViewModel>();

            return this.View(assignmentToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AssignmentEditViewModel input)
        {
            if(!ModelState.IsValid)
            {
                return this.View(input);
            }

            var assignmentToEdit = input.To<AssignmentEditDto>();

            await this.assignmentsService.EditAsync(assignmentToEdit);

            return RedirectToAction("Details", new { id = input.Id });
        }
    }
}