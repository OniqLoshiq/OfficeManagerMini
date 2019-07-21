using System.ComponentModel.DataAnnotations;

namespace OMM.App.Models.InputModels
{
    public class EmployeeLoginInputModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
