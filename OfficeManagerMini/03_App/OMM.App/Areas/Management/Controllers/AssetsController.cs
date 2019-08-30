using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMM.App.Areas.Management.Models.InputModels;
using OMM.App.Areas.Management.Models.ViewModels;
using OMM.App.Common;
using OMM.App.Infrastructure.CustomAuthorization;
using OMM.Services.AutoMapper;
using OMM.Services.Data;
using OMM.Services.Data.DTOs.Assets;

namespace OMM.App.Areas.Management.Controllers
{
    public class AssetsController : BaseController
    {
        private readonly IAssetsService assetsService;

        public AssetsController(IAssetsService assetsService)
        {
            this.assetsService = assetsService;
        }

        [MinimumAccessLevel(AccessLevelValue.Five)]
        public async Task<IActionResult> All()
        {
            var assetList = await this.assetsService.GetAll().To<AssetsListAllViewModel>().ToListAsync();

            return View(assetList);
        }

        [MinimumAccessLevel(AccessLevelValue.Seven)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [MinimumAccessLevel(AccessLevelValue.Seven)]
        public async Task<IActionResult> Create(AssetCreateInputModel input)
        {
            if(!ModelState.IsValid)
            {
                return this.View(input);
            }

            var asset = AutoMapper.Mapper.Map<AssetCreateDto>(input);

            await this.assetsService.CreateAsync(asset);


            return this.RedirectToAction("All");
        }

        [MinimumAccessLevel(AccessLevelValue.Seven)]
        public async Task<IActionResult> Edit(string id)
        {
            var assetToEdit = await this.assetsService.GetAssetByIdAsync(id);

            var assetViewModel = AutoMapper.Mapper.Map<AssetEditViewModel>(assetToEdit);

            return this.View(assetViewModel);
        }

        [HttpPost]
        [MinimumAccessLevel(AccessLevelValue.Seven)]
        public async Task<IActionResult> Edit(AssetEditViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            var assetToEdit = AutoMapper.Mapper.Map<AssetEditDto>(viewModel);

            await this.assetsService.EditAsync(assetToEdit);

            return this.RedirectToAction("All");
        }


        [MinimumAccessLevel(AccessLevelValue.Seven)]
        public async Task<IActionResult> Delete(string id)
        {
            if(id == null)
            {
                return this.RedirectToAction("All");
            }

            await this.assetsService.DeleteAsync(id);


            return this.RedirectToAction("All");
        }
    }
}