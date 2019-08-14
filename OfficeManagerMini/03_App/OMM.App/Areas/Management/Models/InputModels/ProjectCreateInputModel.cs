using OMM.App.Common;
using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Projects;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMM.App.Areas.Management.Models.InputModels
{
    public class ProjectCreateInputModel : IValidatableObject, IMapTo<ProjectCreateDto>
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Client")]
        public string Client { get; set; }

        [Required]
        [Display(Name = "Priority")]
        public int Priority { get; set; }

        [Required]
        [Display(Name = "Creating Date")]
        public string CreatedOn { get; set; }

        [Display(Name = "Deadline")]
        public string Deadline { get; set; }

        [Display(Name = "Project progress:")]
        public double Progress { get; set; }

        [Required]
        [Display(Name = "Status")]
        public int StatusId { get; set; }

        public List<ProjectParticipantInputModel> Participants { get; set; } = new List<ProjectParticipantInputModel>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Participants.Count == 0)
            {
                yield return new ValidationResult(ErrorMessages.INVALID_PARTICIPANTS, new List<string> { "Participants" });
            }
        }
    }
}
