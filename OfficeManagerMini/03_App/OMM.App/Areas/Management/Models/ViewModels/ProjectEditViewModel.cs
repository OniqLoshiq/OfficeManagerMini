using OMM.App.Common;
using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Projects;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OMM.App.Areas.Management.Models.ViewModels
{
    public class ProjectEditViewModel : IValidatableObject, IMapFrom<ProjectEditDto>, IMapTo<ProjectEditDto>
    {
        public string Id { get; set; }

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

        public List<ProjectEditParticipantViewModel> Participants { get; set; } = new List<ProjectEditParticipantViewModel>();

        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            if (this.Participants.Count == 0)
            {
                yield return new ValidationResult(ErrorMessages.INVALID_PARTICIPANTS_COUNT, new List<string> { "Participants" });
            }

            if (this.Participants.Count != this.Participants.Select(p => p.EmployeeId).Distinct().Count())
            {
                yield return new ValidationResult(ErrorMessages.INVALID_PARTICIPANTS_DUPLICATE, new List<string> { "Participants" });
            }

            if (!this.Participants.Any(p => p.ProjectPositionId == Constants.PROJECT_MANAGER_ROLE_ID))
            {
                yield return new ValidationResult(ErrorMessages.INVALID_PARTICIPANTS_MANAGER, new List<string> { "Participants" });
            }
        }
    }
}
