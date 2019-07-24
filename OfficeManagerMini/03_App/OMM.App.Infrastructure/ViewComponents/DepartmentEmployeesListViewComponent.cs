using Microsoft.AspNetCore.Mvc;
using OMM.App.Infrastructure.ViewComponents.Models;
using OMM.Services.AutoMapper;
using OMM.Services.Data;
using System.Linq;

namespace OMM.App.Infrastructure.ViewComponents
{
    public class DepartmentEmployeesListViewComponent : ViewComponent
    {
        private readonly IDepartmentsService departmentsService;

        public DepartmentEmployeesListViewComponent(IDepartmentsService departmentsService)
        {
            this.departmentsService = departmentsService;
        }

        public IViewComponentResult Invoke(string employeeId)
        {
            var departmentsEmployeesList =  this.departmentsService.GetAllWithActiveEmployees().To<DepartmentEmployeesListViewComponentViewModel>().ToList();

            this.ViewData["Id"] = employeeId;

            return View(departmentsEmployeesList);
        }
    }
}
