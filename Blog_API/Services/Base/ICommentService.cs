using Blog_API.DTOs;

namespace Blog_API.Services.Base
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDto>> GetAllCommentsAsync();
        Task<CommentDto> GetCommentByIdAsync(int id);
        Task<CommentDto> AddCommentAsync(CommentDto dto);
        Task<CommentDto> UpdateCommentAsync(int id, CommentDto dto);
        Task<string> DeleteCommentAsync(int id);
        
        Task<IEnumerable<CommentDto>> GetCommentsByPlogIdAsync(int plogId);
        
        Task<IEnumerable<CommentDto>> GetCommentsByUserIdAsync(int userId);
    }
}
