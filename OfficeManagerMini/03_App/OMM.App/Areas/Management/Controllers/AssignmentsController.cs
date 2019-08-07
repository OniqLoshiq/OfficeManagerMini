using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMM.App.Areas.Management.Models.ViewModels;
using OMM.App.Common;
using OMM.Services.AutoMapper;
using OMM.Services.Data;

namespace OMM.App.Areas.Management.Controllers
{
    [Authorize(Roles = "Admin, Management")]
    public class AssignmentsController : BaseController
    {
        private readonly IAssignmentsService assignmentsService;

        public AssignmentsController(IAssignmentsService assignmentsService)
        {
            this.assignmentsService = assignmentsService;
        }

        public async Task<IActionResult> All()
        {
            var allAssignments = new AssignmentsAllListViewModel();

            allAssignments.OngoingAssignments = await this.assignmentsService.GetAllAssignments()
                .Where(a => a.StatusName != Constants.STATUS_COMPLETED)
                .OrderBy(a => a.StatusName == Constants.STATUS_INPROGRESS)
                .ThenByDescending(a => a.Priority)
                .To<AssignmentOngoingListViewModel>()
                .ToListAsync();

            allAssignments.CompletedAssignments = await this.assignmentsService.GetAllAssignments()
                .Where(a => a.StatusName == Constants.STATUS_COMPLETED)
                .OrderByDescending(a => a.EndDate)
                .ThenByDescending(a => a.Priority)
                .To<AssignmentCompletedListViewModel>()
                .ToListAsync();

            return View(allAssignments);
        }
    }
}