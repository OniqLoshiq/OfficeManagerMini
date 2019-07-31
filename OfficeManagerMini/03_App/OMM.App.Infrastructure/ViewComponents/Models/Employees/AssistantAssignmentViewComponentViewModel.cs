using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace OMM.App.Infrastructure.ViewComponents.Models.Employees
{
    public class AssistantAssignmentViewComponentViewModel
    {
        public List<string> AssistantsIds { get; set; } = new List<string>();

        public List<SelectListItem> Assistants { get; set; } = new List<SelectListItem>();
    }
}
