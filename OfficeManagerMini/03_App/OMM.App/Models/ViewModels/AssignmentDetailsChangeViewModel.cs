using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Assignments;
using System.ComponentModel.DataAnnotations;

namespace OMM.App.Models.ViewModels
{
    public class AssignmentDetailsChangeViewModel : IMapFrom<AssignmentDetailsChangeDto>, IMapTo<AssignmentDetailsChangeDto>
    {
        public string EndDate { get; set; }

        public string Deadline { get; set; }

        [Required]
        public double Progress { get; set; }

        [Required]
        public string StatusId { get; set; }
        
        public string StatusName { get; set; }
    }
}
