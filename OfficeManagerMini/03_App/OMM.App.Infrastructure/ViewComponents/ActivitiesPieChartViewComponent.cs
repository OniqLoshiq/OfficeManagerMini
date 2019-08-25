using Highsoft.Web.Mvc.Charts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMM.App.Infrastructure.ViewComponents.Models.Activities;
using OMM.Services.AutoMapper;
using OMM.Services.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMM.App.Infrastructure.ViewComponents
{
    public class ActivitiesPieChartViewComponent : ViewComponent
    {
        private readonly IActivitiesService activitiesService;

        public ActivitiesPieChartViewComponent(IActivitiesService activitiesService)
        {
            this.activitiesService = activitiesService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string reportId)
        {
            var vm = new ActivitiesPieSeriesDataViewComponentViewModel
            {
                ReportId = reportId
            };

            var activities = await this.activitiesService.GetActivitiesByReportId(reportId).To<ActivityPieDataViewModel>().ToListAsync();
            int totalActivitiesTime = activities.Sum(a => a.WorkingMinutes);
            List<string> employeeIds = activities.Select(a => a.EmployeeId).Distinct().ToList();

            for (int i = 0; i < employeeIds.Count; i++)
            {
                var emp = activities.Where(a => a.EmployeeId == employeeIds[i]).Select(a => a.EmployeeFullName).First();
                vm.PieData.Add(new PieSeriesData { Name = emp, Y = activities.Where(a => a.EmployeeId == employeeIds[i]).Sum(a => a.WorkingMinutes) });
            }


            return View(vm);
        }
    }
}
