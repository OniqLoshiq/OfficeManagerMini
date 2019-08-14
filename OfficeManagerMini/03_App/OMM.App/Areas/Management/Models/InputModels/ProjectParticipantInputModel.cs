using OMM.App.Common;
using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Projects;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMM.App.Areas.Management.Models.InputModels
{
    public class ProjectParticipantInputModel : IValidatableObject, IMapTo<ProjectParticipantDto>
    {
        [Required]
        [Display(Name = "Project participant")]
        public string EmployeeId { get; set; }

        [Required]
        [Display(Name = "Project position")]
        public int ProjectPositionId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.ProjectPositionId == 0)
            {
                yield return new ValidationResult(ErrorMessages.INVALID_PROJECTPOSITION, new List<string> { "ProjectPositionId" });
            }
        }
    }
}
