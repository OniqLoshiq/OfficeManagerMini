using Highsoft.Web.Mvc.Charts;
using System.Collections.Generic;

namespace OMM.App.Infrastructure.ViewComponents.Models.Activities
{
    public class ActivitiesPieSeriesDataViewComponentViewModel
    {
        public string ReportId { get; set; }

        public List<PieSeriesData> PieData { get; set; } = new List<PieSeriesData>();
    }
}
