using Microsoft.AspNetCore.Mvc.Rendering;

namespace OMM.App.Infrastructure.ViewComponents.Models.Statuses
{
    public class StatusesListViewComponentViewModel
    {
        public int StatusId { get; set; }

        public SelectList Statuses { get; set; }
    }
}
