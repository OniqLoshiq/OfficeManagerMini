using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OMM.App.Infrastructure.ViewComponents.Models.LeavingReasons;
using OMM.Services.Data;
using System.Linq;

namespace OMM.App.Infrastructure.ViewComponents
{
    public class LeavingReasonsListViewComponent : ViewComponent
    {
        private readonly ILeavingReasonsService leavingReasonsService;

        public LeavingReasonsListViewComponent(ILeavingReasonsService leavingReasonsService)
        {
            this.leavingReasonsService = leavingReasonsService;
        }

        public IViewComponentResult Invoke(int leavingReasonId)
        {
            var vm = new LeavingReasonViewComponentViewModel
            {
                LeavingReasonId = leavingReasonId
            };

            vm.LeavingReasons = new SelectList(this.leavingReasonsService.GetLeavingReasons().ToList(), "Id", "Reason");

            return View(vm);
        }
    }
}
