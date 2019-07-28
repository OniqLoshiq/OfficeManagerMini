using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Employees;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMM.App.Areas.Management.Models.ViewModels
{
    public class EmployeeReleaseViewModel : IMapFrom<EmployeeReleaseDto>, IMapTo<EmployeeReleaseDto>
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Left on")]
        public string LeftOn { get; set; }

        [Required]
        [Display(Name = "Leaving Reason")]
        public int LeavingReasonId { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle name")]
        public string MiddleName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Full name")]
        public string FullName { get; set; }

        [Display(Name = "Date of birth")]
        public string DateOfBirth { get; set; }

        [Display(Name = "Pers. phone number")]
        public string PersonalPhoneNumber { get; set; }

        public string ProfilePicture { get; set; }

        [Display(Name = "Access level")]
        public int AccessLevel { get; set; }

        [Display(Name = "Department")]
        public string DepartmentName { get; set; }

        [Display(Name = "Position")]
        public string Position { get; set; }

        [Display(Name = "Appointed on")]
        public string AppointedOn { get; set; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public List<AssetEmployeeViewModel> Items { get; set; } = new List<AssetEmployeeViewModel>();
    }
}
