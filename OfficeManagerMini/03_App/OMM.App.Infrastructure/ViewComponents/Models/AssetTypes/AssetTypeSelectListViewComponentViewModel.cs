using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace OMM.App.Infrastructure.ViewComponents.Models.AssetTypes
{
    public class AssetTypeSelectListViewComponentViewModel
    {
        public int AssetTypeId { get; set; }

        public List<SelectListItem> AssetTypes { get; set; } = new List<SelectListItem>();
    }
}
