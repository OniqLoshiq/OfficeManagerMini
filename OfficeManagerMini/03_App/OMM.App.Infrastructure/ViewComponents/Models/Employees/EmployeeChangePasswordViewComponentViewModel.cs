using OMM.App.Infrastructure.Common;
using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Employees;
using System.ComponentModel.DataAnnotations;

namespace OMM.App.Infrastructure.ViewComponents.Models.Employees
{
    public class EmployeeChangePasswordViewComponentViewModel : IMapTo<EmployeeChangePasswordDto>
    {
        [Required]
        [Display(Name = "Current Password")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required]
        [Display(Name = "New Password")]
        [StringLength(20, ErrorMessage = InfrastructureConstants.PASSWORD_LENGHT, MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [Display(Name = "Confirm new Password")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = InfrastructureConstants.PASSWORD_NOT_MATCH)]
        public string ConfirmNewPassword { get; set; }
    }
}
