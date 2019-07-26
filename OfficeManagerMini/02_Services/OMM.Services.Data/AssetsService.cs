using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OMM.Data;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.Common;
using OMM.Services.Data.DTOs.Assets;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace OMM.Services.Data
{
    public class AssetsService : IAssetsService
    {
        private readonly OmmDbContext context;

        public AssetsService(OmmDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreateAsync(AssetCreateDto assetModel)
        {
            var asset = Mapper.Map<Asset>(assetModel);

            this.context.Assets.Add(asset);

            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public IQueryable<AssetListDto> GetAll()
        {
            return this.context.Assets.To<AssetListDto>();
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var asset = this.context.Assets.SingleOrDefault(a => a.Id == id);

            //TODO:
            //if(asset == null)
            //{
            //    throw new System.Exception();
            //}

            this.context.Assets.Remove(asset);

            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<AssetEditDto> GetAssetByIdAsync(string id)
        {
            var asset = await this.context.Assets.Where(a => a.Id == id).To<AssetEditDto>().FirstOrDefaultAsync();
            
            return asset;
        }

        public async Task<bool> EditAsync(AssetEditDto assetToEdit)
        {
            var asset = await this.context.Assets.FirstOrDefaultAsync(a => a.Id == assetToEdit.Id);

            //TODO:
            //if(asset == null)
            //{
            //    throw new System.Exception();
            //}

            asset.InventoryNumber = assetToEdit.InventoryNumber;
            asset.Make = assetToEdit.Make;
            asset.Model = assetToEdit.Model;
            asset.ReferenceNumber = assetToEdit.ReferenceNumber;
            asset.AssetTypeId = assetToEdit.AssetTypeId;
            asset.EmployeeId = assetToEdit.EmployeeId;
            asset.DateOfAquire = DateTime.ParseExact(assetToEdit.DateOfAquire, Constants.DATETIME_FORMAT, CultureInfo.InvariantCulture);

            this.context.Assets.Update(asset);

            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public IQueryable<AssetEmployeeDto> GetAssetsByEmployeeId(string employeeId)
        {
            var assets = this.context.Assets.Where(a => a.EmployeeId == employeeId).To<AssetEmployeeDto>();

            return assets;
        }
    }
}
