using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Comments;
using System;
using System.ComponentModel.DataAnnotations;

namespace OMM.App.Models.ViewModels
{
    public class CommentEditViewModel : IMapTo<CommentEditDto>
    {
        public string Id { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string AssignmentId { get; set; }
    }
}
