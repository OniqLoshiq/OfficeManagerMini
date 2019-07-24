using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OMM.App.Infrastructure.ViewComponents.Models;
using OMM.App.Infrastructure.ViewComponents.Models.AssetTypes;
using OMM.Services.AutoMapper;
using OMM.Services.Data;
using System.Threading.Tasks;

namespace OMM.App.Infrastructure.ViewComponents
{
    public class AssetTypeSelectListViewComponent : ViewComponent
    {
        private readonly IAssetTypesService assetTypesService;

        public AssetTypeSelectListViewComponent(IAssetTypesService assetTypesService)
        {
            this.assetTypesService = assetTypesService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int assetTypeId)
        {
            var vm = new AssetTypeSelectListViewComponentViewModel
            {
                AssetTypeId = assetTypeId
            };

            var assetTypesToSelectList = await this.assetTypesService.GetAll().To<AssetTypeSelectItemViewModel>().ToListAsync();

            assetTypesToSelectList.ForEach(at => vm.AssetTypes.Add(new SelectListItem { Value = at.Id.ToString(), Text = at.Name }));

            return View(vm);
        }
    }
}
