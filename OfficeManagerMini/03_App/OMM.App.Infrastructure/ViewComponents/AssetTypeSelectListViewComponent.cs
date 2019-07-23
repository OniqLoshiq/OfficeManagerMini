using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMM.App.Infrastructure.ViewComponents.Models;
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

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var assetTypesToSelectList = await this.assetTypesService.GetAll().To<AssetTypeSelectListViewComponentViewModel>().ToListAsync();

            return View(assetTypesToSelectList);
        }
    }
}
