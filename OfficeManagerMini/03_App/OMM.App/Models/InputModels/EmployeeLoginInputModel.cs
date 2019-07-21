using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Employees;
using System.ComponentModel.DataAnnotations;

namespace OMM.App.Models.InputModels
{
    public class EmployeeLoginInputModel : IMapTo<EmployeeLoginDto>
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
