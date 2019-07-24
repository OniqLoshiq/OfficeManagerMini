using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OMM.App.Infrastructure.ViewComponents.Models.Departments;
using OMM.Services.AutoMapper;
using OMM.Services.Data;
using System.Linq;

namespace OMM.App.Infrastructure.ViewComponents
{
    public class DepartmentsListViewComponent : ViewComponent
    {
        private readonly IDepartmentsService departmentsService;

        public DepartmentsListViewComponent(IDepartmentsService departmentsService)
        {
            this.departmentsService = departmentsService;
        }

        public IViewComponentResult Invoke(int departmentId)
        {
            var vm = new DepartmentViewComponentViewModel
            {
                DepartmentId = departmentId
            };

            vm.Departments = new SelectList(this.departmentsService.GetAllDepartmentsList().To<DepartmentListViewModel>().ToList(), "Id", "Name");

            return View(vm);
        }
    }
}
