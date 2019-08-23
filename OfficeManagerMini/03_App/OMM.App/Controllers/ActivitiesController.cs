using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OMM.App.Models.InputModels;
using OMM.Services.AutoMapper;
using OMM.Services.Data;
using OMM.Services.Data.DTOs.Activities;

namespace OMM.App.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly IActivitiesService activitiesService;

        public ActivitiesController(IActivitiesService activitiesService)
        {
            this.activitiesService = activitiesService;
        }

        public IActionResult Create(string reportId)
        {
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = new ActivityCreateInputModel
            {
                ReportId = reportId,
                EmployeeId = currentUserId
            };

            return PartialView("_ActivityCreatePartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ActivityCreateInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_ActivityCreatePartial", input);
            }
            var activityToCreate = input.To<ActivityCreateDto>();

            await this.activitiesService.CreateActivityAsync(activityToCreate);

            var redirectUrl = Url.Action("Details", "Reports", new { id = input.ReportId });

            return Json(new { success = true, url = redirectUrl });
        }
    }
}