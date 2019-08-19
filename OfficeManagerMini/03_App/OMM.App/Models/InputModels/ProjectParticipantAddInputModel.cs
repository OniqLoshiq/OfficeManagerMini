using OMM.App.Common;
using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Projects;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMM.App.Models.InputModels
{
    public class ProjectParticipantAddInputModel : IValidatableObject, IMapTo<ProjectParticipantAddDto>
    {
        [Required]
        public string EmployeeId { get; set; }

        public string ProjectId { get; set; }

        [Required]
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
