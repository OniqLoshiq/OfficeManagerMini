using Microsoft.AspNetCore.Mvc.Rendering;

namespace OMM.App.Infrastructure.ViewComponents.Models.Departments
{
    public class DepartmentViewComponentViewModel
    {
        public int DepartmentId { get; set; }

        public SelectList Departments { get; set; } 
    }
}
