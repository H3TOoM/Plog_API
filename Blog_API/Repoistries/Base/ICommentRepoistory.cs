using Blog_API.DTOs;

namespace Blog_API.Repoistries.Base
{
    public interface ICommentRepoistory
    {
       
        Task<IEnumerable<CommentDto>> GetCommentsByPlogIdAsync(int plogId);
        
        Task<IEnumerable<CommentDto>> GetCommentsByUserIdAsync(int userId);
    }
}
