using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Projects;
using System.ComponentModel.DataAnnotations;

namespace OMM.App.Areas.Management.Models.InputModels
{
    public class ProjectParticipantInputModel : IMapTo<ProjectParticipantDto>
    {
        [Required]
        public string EmployeeId { get; set; }

        [Required]
        public string ProjectPositionId { get; set; }
    }
}
