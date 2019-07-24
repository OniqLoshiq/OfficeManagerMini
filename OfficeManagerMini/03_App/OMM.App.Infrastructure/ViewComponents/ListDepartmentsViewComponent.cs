using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMM.App.Infrastructure.ViewComponents.Models.Departments;
using OMM.Services.AutoMapper;
using OMM.Services.Data;
using System.Threading.Tasks;

namespace OMM.App.Infrastructure.ViewComponents
{
    public class ListDepartmentsViewComponent : ViewComponent
    {
        private readonly IDepartmentsService departmentsService;

        public ListDepartmentsViewComponent(IDepartmentsService departmentsService)
        {
            this.departmentsService = departmentsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var departmentsToList = await this.departmentsService.GetAllDepartmentNames().To<DepartmentViewComponentViewModel>().ToListAsync();

            return View(departmentsToList);
        }
    }
}
