using Blog_API.Data;
using Blog_API.DTOs;
using Blog_API.Repoistries.Base;
using Microsoft.EntityFrameworkCore;

namespace Blog_API.Repoistries
{
    public class CommentRepoistory : ICommentRepoistory
    {
        // inject AppDbContext
        private readonly AppDbContext _context;
        public CommentRepoistory(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CommentDto>> GetCommentsByPlogIdAsync( int plogId )
        {
            var comments = await _context.Comments
                .Where(c => c.PlogId == plogId)
                .Select(c => new CommentDto
                {
                    Id = c.Id,
                    Content = c.Content,
                    UserId = c.UserId,
                    PlogId = c.PlogId,
                    CreatedAt = c.CreatedAt
                })
                .ToListAsync();
            return comments;

        }

        public async Task<IEnumerable<CommentDto>> GetCommentsByUserIdAsync( int userId )
        {
            var comments = await _context.Comments
                .Where(c => c.UserId == userId)
                .Select(c => new CommentDto
                {
                    Id = c.Id,
                    Content = c.Content,
                    UserId = c.UserId,
                    PlogId = c.PlogId,
                    CreatedAt = c.CreatedAt
                })
                .ToListAsync();
            return comments;
        }
    }
}
