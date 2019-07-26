using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMM.App.Infrastructure.ViewComponents.Models.Assets;
using OMM.Services.AutoMapper;
using OMM.Services.Data;
using System.Threading.Tasks;

namespace OMM.App.Infrastructure.ViewComponents
{
    public class EmployeeAssetsViewComponent : ViewComponent
    {
        private readonly IAssetsService assetsService;

        public EmployeeAssetsViewComponent(IAssetsService assetsService)
        {
            this.assetsService = assetsService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string employeeId)
        {
            var assets = await this.assetsService.GetAssetsByEmployeeId(employeeId).To<EmployeeAssetViewComponentViewModel>().ToListAsync();

            return this.View(assets);
        }
    }
}
