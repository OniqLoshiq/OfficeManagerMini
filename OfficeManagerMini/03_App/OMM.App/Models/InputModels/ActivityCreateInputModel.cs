using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Activities;
using System.ComponentModel.DataAnnotations;

namespace OMM.App.Models.InputModels
{
    public class ActivityCreateInputModel : IMapTo<ActivityCreateDto>
    {
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Date")]
        public string Date { get; set; }

        [Required]
        [Display(Name = "Working time")]
        public string WorkingTime { get; set; }
        
        [Required]
        public string EmployeeId { get; set; }

        [Required]
        public string ReportId { get; set; }
    }
}
