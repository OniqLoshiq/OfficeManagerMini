using Microsoft.AspNetCore.Mvc;
using OMM.App.Infrastructure.ViewComponents.Models.Employees;

namespace OMM.App.Infrastructure.ViewComponents
{
    public class EmployeeChangePasswordViewComponent : ViewComponent
    {
        public EmployeeChangePasswordViewComponent()
        {
        }

        public IViewComponentResult Invoke()
        {
            var vm = new EmployeeChangePasswordViewComponentViewModel();

            return View(vm);
        }
    }
}
