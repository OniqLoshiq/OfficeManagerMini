using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Assets;
using System;
using System.ComponentModel.DataAnnotations;

namespace OMM.App.Areas.Management.Models.InputModels
{
    public class AssetCreateInputModel : IMapTo<AssetCreateDto>
    {
        [Required]
        [Display(Name = "Inventory number")]
        public string InventoryNumber { get; set; }

        [Required]
        [Display(Name = "Make")]
        public string Make { get; set; }

        [Required]
        [Display(Name = "Model")]
        public string Model { get; set; }

        [Required]
        [Display(Name = "Reference number")]
        public string ReferenceNumber { get; set; }

        [Required]
        [Display(Name = "Aquired on")]
        public string DateOfAquire { get; set; }

        [Required]
        [Display(Name = "Asset type")]
        public int AssetTypeId { get; set; }

        [Display(Name = "Employee")]
        public string EmployeeId { get; set; }
    }
}
