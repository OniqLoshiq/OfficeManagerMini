using OMM.Services.Data.DTOs.Comments;
using System.Threading.Tasks;

namespace OMM.Services.Data
{
    public interface ICommentsService
    {
        Task<bool> CreateAsync(CommentCreateDto input);

        Task<bool> EditAsync(CommentEditDto input);

        Task<bool> DeleteAsync(string id);
    }
}
