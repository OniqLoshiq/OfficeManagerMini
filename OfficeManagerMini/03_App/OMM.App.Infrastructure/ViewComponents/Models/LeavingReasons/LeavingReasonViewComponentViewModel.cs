using Microsoft.AspNetCore.Mvc.Rendering;

namespace OMM.App.Infrastructure.ViewComponents.Models.LeavingReasons
{
    public class LeavingReasonViewComponentViewModel
    {
        public int LeavingReasonId { get; set; }

        public SelectList LeavingReasons { get; set; }
    }
}
