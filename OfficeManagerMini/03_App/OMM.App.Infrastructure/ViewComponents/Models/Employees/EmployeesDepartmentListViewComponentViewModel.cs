using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace OMM.App.Infrastructure.ViewComponents.Models.Employees
{
    public class EmployeesDepartmentListViewComponentViewModel
    {
        public string EmployeeId { get; set; }

        public List<SelectListItem> Employees { get; set; } = new List<SelectListItem>();
    }
}
