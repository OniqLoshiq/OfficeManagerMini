using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OMM.App.Models.InputModels;
using OMM.App.Models.ViewModels;
using OMM.Services.AutoMapper;
using OMM.Services.Data;
using OMM.Services.Data.DTOs.Comments;

namespace OMM.App.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentsService commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CommentCreateInputModel input)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("Details", "Assignments", new { id = input.AssignmentId });
            }

            input.CreatedOn = DateTime.UtcNow;

            var comment = input.To<CommentCreateDto>();

            await this.commentsService.CreateAsync(comment);

            return RedirectToAction("Details", "Assignments", new { id = input.AssignmentId });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CommentEditViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", "Assignments", new { id = input.AssignmentId });
            }

            input.ModifiedOn = DateTime.UtcNow;

            var comment = input.To<CommentEditDto>();

            await this.commentsService.EditAsync(comment);

            return RedirectToAction("Details", "Assignments", new { id = input.AssignmentId });
        }

        public async Task<IActionResult> Delete(string id, string assignmentId)
        {
            if (id == null)
            {
                return RedirectToAction("Details", "Assignments", new { id = assignmentId });
            }

            await this.commentsService.DeleteAsync(id);


            return RedirectToAction("Details", "Assignments", new { id = assignmentId });
        }

    }
}