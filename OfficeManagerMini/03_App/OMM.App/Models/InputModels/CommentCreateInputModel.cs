using OMM.Services.AutoMapper;
using OMM.Services.Data.DTOs.Comments;
using System;
using System.ComponentModel.DataAnnotations;

namespace OMM.App.Models.InputModels
{
    public class CommentCreateInputModel : IMapTo<CommentCreateDto>
    {
        [Required]
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AssignmentId { get; set; }

        public string CommentatorId { get; set; }
    }
}
