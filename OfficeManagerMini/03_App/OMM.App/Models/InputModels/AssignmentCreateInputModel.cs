using AutoMapper;
using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Assignments;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OMM.App.Models.InputModels
{
    public class AssignmentCreateInputModel : IMapTo<AssignmentCreateDto>, IHaveCustomMappings
    {
        [Required]
        [Display(Name = "Title")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string Type { get; set; }

        [Required]
        [Display(Name = "Priority")]
        [Range(1, 10)]
        public int Priority { get; set; }

        [Required]
        [Display(Name = "Is Project Related")]
        public bool IsProjectRelated { get; set; }

        [Required]
        [Display(Name = "Starting date")]
        public string StartingDate { get; set; }

        [Display(Name = "Deadline")]
        public string Deadline { get; set; }

        [Required]
        [Display(Name = "Assignment progress:")]
        public double Progress { get; set; }

        [Required]
        [Display(Name = "Status")]
        public int StatusId { get; set; }

        [Required]
        [Display(Name = "Assignor")]
        public string AssignorId { get; set; }

        [Required]
        [Display(Name = "Executor")]
        public string EmployeeId { get; set; }

        [Display(Name = "Project")]
        public string ProjectId { get; set; }

        [Display(Name = "Assistants")]
        public List<string> AssistantsIds { get; set; } = new List<string>();

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<AssignmentCreateInputModel, AssignmentCreateDto>()
                .ForMember(destination => destination.ExecutorId,
                opts => opts.MapFrom(origin => origin.EmployeeId));
        }
    }
}
