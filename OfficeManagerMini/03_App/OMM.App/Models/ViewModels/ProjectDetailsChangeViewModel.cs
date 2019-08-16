using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Projects;
using System.ComponentModel.DataAnnotations;

namespace OMM.App.Models.ViewModels
{
    public class ProjectDetailsChangeViewModel : IMapFrom<ProjectDetailsChangeDto>, IMapTo<ProjectDetailsChangeDto>
    {
        public string Id { get; set; }

        public string EndDate { get; set; }

        public string Deadline { get; set; }

        [Required]
        public double Progress { get; set; }

        [Required]
        [Display(Name = "Status")]
        public int StatusId { get; set; }

        public string StatusName { get; set; }
    }
}
