using Microsoft.AspNetCore.Http;
using OMM.App.Common;
using OMM.App.Infrastructure.Attributes;
using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Employees;
using System.ComponentModel.DataAnnotations;

namespace OMM.App.Areas.Management.Models.InputModels
{
    public class EmployeeRegisterInputModel : IMapTo<EmployeeRegisterDto>
    {
        public string Username => Email;

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        [IsUniqueUserProperty(nameof(Email), ErrorMessage = ErrorMessages.NOT_UNIQUE_EMAIL)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle name")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Full name")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Date of birth")]
        public string DateOfBirth { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Pers. phone number")]
        public string PersonalPhoneNumber { get; set; }

        [Required]
        [Display(Name = "Profile picture")]
        public IFormFile ProfilePicture { get; set; }

        [Required]
        [Display(Name = "Access level")]
        [Range(0,10)]
        public int AccessLevel { get; set; }

        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [Required]
        [Display(Name = "Position")]
        public string Position { get; set; }

        [Required]
        [Display(Name = "Appointed on")]
        public string AppointedOn { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }
}
