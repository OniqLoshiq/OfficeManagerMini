using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OMM.App.Infrastructure.ViewComponents.Models.Statuses;
using OMM.Services.AutoMapper;
using OMM.Services.Data;

namespace OMM.App.Infrastructure.ViewComponents
{
    public class StatusesListViewComponent : ViewComponent
    {
        private readonly IStatusesService statusesService;

        public StatusesListViewComponent(IStatusesService statusesService)
        {
            this.statusesService = statusesService;
        }

        public IViewComponentResult Invoke(int statusId)
        {
            var vm = new StatusesListViewComponentViewModel();

            vm.StatusId = statusId;

            vm.Statuses = new SelectList(this.statusesService.GetAllStatuses().To<StatusListViewModel>(), "Id", "Name");

            return View(vm);
        }
    }
}
