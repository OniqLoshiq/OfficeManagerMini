using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OMM.App.Areas.Management.Models.InputModels;
using OMM.App.Common;
using OMM.App.Infrastructure.CustomAuthorization;
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

        public IActionResult All()
        {
            return View();
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
                return this.View();
            }

            var asset = AutoMapper.Mapper.Map<AssetCreateDto>(input);

            await this.assetsService.CreateAsync(asset);


            return this.RedirectToAction("All");
        }
    }
}