﻿using Microsoft.AspNetCore.Mvc;
using OMM.App.Models.ViewModels;
using OMM.Services.AutoMapper;
using OMM.Services.Data;
using OMM.Services.Data.DTOs.Reports;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OMM.App.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IReportsService reportsService;
        private readonly IProjectsService projectsService;

        public ReportsController(IReportsService reportsService, IProjectsService projectsService)
        {
            this.reportsService = reportsService;
            this.projectsService = projectsService;
        }

        public async Task<IActionResult> Details(string id)
        {
            var report =  this.reportsService.GetReportById<ReportDetailsDto>(id).SingleOrDefault().To<ReportDetailsViewModel>();
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var isEmployeeAuthorizedForProject = await this.projectsService.IsEmployeeAuthorizedForProject(report.Project.Id, currentUserId);

            if(!isEmployeeAuthorizedForProject)
            {
                return Forbid();
            }

            var isEmployeeAuthorizedToChangeProject = await this.projectsService.IsEmployeeAuthorizedToChangeProject(report.Project.Id, currentUserId);

            ViewBag.IsEmployeeAuthorizeToChange = isEmployeeAuthorizedToChangeProject;
            ViewBag.CurrentUserId = currentUserId;

            if (!isEmployeeAuthorizedToChangeProject)
            {
                report.Activities = report.Activities.Where(a => a.EmployeeId == currentUserId).OrderByDescending(a => a.Date).ToList();

                return View(report);
            }

            report.Activities = report.Activities.OrderByDescending(a => a.Date).ToList();

            return View(report);
        }
    }
}