using Microsoft.EntityFrameworkCore;
using OMM.Data;
using OMM.Domain;
using OMM.Services.AutoMapper;
using OMM.Services.Data;
using OMM.Services.Data.Common;
using OMM.Services.Data.DTOs.Comments;
using OMM.Tests.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace OMM.Tests.Services
{
    public class CommentsServiceTests
    {
        private const string Comment_Id_1 = "1";
        private const string Comment_Id_2 = "2";

        private const string Description_1 = "Some random description 1";
        private const string Description_2 = "Some random description 2";

        private const string Employee_Id_1 = "001";
        private const string Employee_Id_2 = "002";

        private const string Employee_FullName_1 = "Test employee 1";
        private const string Employee_FullName_2 = "Test employee 2";

        private const string Assignment_Id_1 = "01";
        private const string Assignment_Id_2 = "02";

        private const string Assignment_Name_1 = "Test assignment 1";
        private const string Assignment_Name_2 = "Test assignment 2";

        private ICommentsService commentsService;

        private List<Comment> GetDummyData()
        {
            return new List<Comment>
            {
                new Comment
                {
                    Id = Comment_Id_1,
                    Description = Description_1,
                    CreatedOn = DateTime.UtcNow.AddDays(-10),
                    ModifiedOn = null,
                    AssignmentId = Assignment_Id_1,
                    Assignment = new Assignment { Id = Assignment_Id_1, Name = Assignment_Name_1},
                    CommentatorId = Employee_Id_1,
                    Commentator = new Employee { Id = Employee_Id_1, FirstName = Employee_FullName_1}
                },
                new Comment
                {
                    Id = Comment_Id_2,
                    Description = Description_2,
                    CreatedOn = DateTime.UtcNow.AddDays(-5),
                    ModifiedOn = null,
                    AssignmentId = Assignment_Id_2,
                    Assignment = new Assignment { Id = Assignment_Id_2, Name = Assignment_Name_2},
                    CommentatorId = Employee_Id_1,
                    Commentator = new Employee { Id = Employee_Id_2, FirstName = Employee_FullName_2}
                }
            };
        }

        private async Task SeedData(OmmDbContext context)
        {
            await context.AddRangeAsync(GetDummyData());
            await context.SaveChangesAsync();
        }

        public CommentsServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public async Task CreateAsync_WithValidData_ShouldCreateCommentAndReturnTrue()
        {
            string errorMessagePrefix = "CommentsService CreateAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.commentsService = new CommentsService(context);

            var commentToCreate = new CommentCreateDto
            {
                Description = "Create new commnet",
                CreatedOn = DateTime.UtcNow,
                AssignmentId = Assignment_Id_1,
                CommentatorId = Employee_Id_1,
            };

            bool actualResult = await this.commentsService.CreateAsync(commentToCreate);

            Assert.True(actualResult, errorMessagePrefix);
        }

        [Fact]
        public async Task CreateAsync_WithInvalidAssignmnetId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.commentsService = new CommentsService(context);

            var commentToCreate = new CommentCreateDto
            {
                Description = "Create new commnet",
                CreatedOn = DateTime.UtcNow,
                AssignmentId = "InvalidId",
                CommentatorId = Employee_Id_1,
            };

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.commentsService.CreateAsync(commentToCreate));

            Assert.Equal(string.Format(ErrorMessages.AssignmentIdNullReference, commentToCreate.AssignmentId), ex.Message);
        }

        [Fact]
        public async Task CreateAsync_WithInvalidCommentatorId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.commentsService = new CommentsService(context);

            var commentToCreate = new CommentCreateDto
            {
                Description = "Create new commnet",
                CreatedOn = DateTime.UtcNow,
                AssignmentId = Assignment_Id_1,
                CommentatorId = "InvalidId",
            };

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.commentsService.CreateAsync(commentToCreate));

            Assert.Equal(string.Format(ErrorMessages.EmployeeIdNullReference, commentToCreate.CommentatorId), ex.Message);
        }

        [Fact]
        public async Task EditAsync_WithValidData_ShouldEditCommentAndReturnTrue()
        {
            string errorMessagePrefix = "CommentsService EditAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.commentsService = new CommentsService(context);

            CommentEditDto expectedResult = (await context.Comments.FirstAsync()).To<CommentEditDto>();

            expectedResult.ModifiedOn = DateTime.UtcNow;
            expectedResult.Description = "New description when edited";

            await this.commentsService.EditAsync(expectedResult);

            var actualResult = (await context.Comments.FirstAsync()).To<CommentEditDto>();

            Assert.True(expectedResult.ModifiedOn == actualResult.ModifiedOn, errorMessagePrefix + " " + "ModifiedOn is not created properly.");
            Assert.True(expectedResult.Description == actualResult.Description, errorMessagePrefix + " " + "Description is not changed properly.");
        }

        [Fact]
        public async Task EditAsync_WithInvalidCommentId_ShouldEditCommentAndReturnTrue()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.commentsService = new CommentsService(context);

            CommentEditDto expectedResult = (await context.Comments.FirstAsync()).To<CommentEditDto>();

            expectedResult.ModifiedOn = DateTime.UtcNow;
            expectedResult.Description = "New description when edited";
            expectedResult.Id = "Invalid Id";

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.commentsService.EditAsync(expectedResult));

            Assert.Equal(string.Format(ErrorMessages.CommentIdNullReference, expectedResult.Id), ex.Message);
        }

        [Theory]
        [InlineData(Comment_Id_1)]
        [InlineData(Comment_Id_2)]
        public async Task DeleteAsync_WithValidId_ShouldDeleteCommentAndReturnTrue(string commentId)
        {
            string errorMessagePrefix = "CommentsService DeleteAsync() method does not work properly.";

            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.commentsService = new CommentsService(context);

            bool actualResult = await this.commentsService.DeleteAsync(commentId);

            Assert.True(actualResult, errorMessagePrefix);
        }

        [Fact]
        public async Task DeleteAsync_WithInvalidId_ShouldThrowNullReferenceException()
        {
            var context = OmmDbContextInMemoryFactory.InitializeContext();
            await SeedData(context);
            this.commentsService = new CommentsService(context);

            string invalidId = "Invalid Id";

            var ex = await Assert.ThrowsAsync<NullReferenceException>(() => this.commentsService.DeleteAsync(invalidId));

            Assert.Equal(string.Format(ErrorMessages.CommentIdNullReference, invalidId), ex.Message);
        }
    }
}
