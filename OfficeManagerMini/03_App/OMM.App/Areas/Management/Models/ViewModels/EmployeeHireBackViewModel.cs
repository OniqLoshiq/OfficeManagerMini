﻿using Microsoft.AspNetCore.Http;
using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Employees;
using System.ComponentModel.DataAnnotations;

namespace OMM.App.Areas.Management.Models.ViewModels
{
    public class EmployeeHireBackViewModel : IMapFrom<EmployeeHireBackDto>, IMapTo<EmployeeHireBackDto>
    {
        public string Id { get; set; }

        public string Username => Email;

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
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

        public string ProfilePicture { get; set; }

        [Display(Name = "Profile picture")]
        public IFormFile ProfilePictureNew { get; set; }

        [Required]
        [Display(Name = "Access level")]
        [Range(0, 10)]
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

        [Display(Name = "Left on")]
        public string LeftOn { get; set; }

        [Display(Name = "Leaving Reason")]
        public string LeavingReasonReason { get; set; }
    }
}
