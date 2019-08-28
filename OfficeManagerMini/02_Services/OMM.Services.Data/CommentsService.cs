using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OMM.Data;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data.Common;
using OMM.Services.Data.DTOs.Comments;

namespace OMM.Services.Data
{
    public class CommentsService : ICommentsService
    {
        private readonly OmmDbContext context;

        public CommentsService(OmmDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreateAsync(CommentCreateDto input)
        {
            var assignment = await this.context.Assignments.AnyAsync(a => a.Id == input.AssignmentId);

            if(!assignment)
            {
                throw new NullReferenceException(string.Format(ErrorMessages.AssignmentIdNullReference, input.AssignmentId));
            }

            var commentator = await this.context.Users.AnyAsync(e => e.Id == input.CommentatorId);

            if(!commentator)
            {
                throw new NullReferenceException(string.Format(ErrorMessages.EmployeeIdNullReference, input.CommentatorId));
            }

            var comment = input.To<Comment>();

            await this.context.Comments.AddAsync(comment);

            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> EditAsync(CommentEditDto input)
        {
            var comment = await this.context.Comments.FirstOrDefaultAsync(c => c.Id == input.Id);

            if(comment == null)
            {
                throw new NullReferenceException(string.Format(ErrorMessages.CommentIdNullReference, input.Id));
            }

            comment.Description = input.Description;
            comment.ModifiedOn = input.ModifiedOn;

            this.context.Comments.Update(comment);
            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var comment = await this.context.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null)
            {
                throw new NullReferenceException(string.Format(ErrorMessages.CommentIdNullReference, id));
            }

            this.context.Comments.Remove(comment);
            var result = await this.context.SaveChangesAsync();

            return result > 0;
        }
    }
}
