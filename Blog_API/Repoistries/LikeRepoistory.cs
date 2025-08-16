using Blog_API.Data;
using Blog_API.Models;
using Blog_API.Repoistries.Base;
using Microsoft.EntityFrameworkCore;

namespace Blog_API.Repoistries
{
    public class LikeRepoistory : ILikeRepoistory
    {

        // inject AppDbContext
        private readonly AppDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        public LikeRepoistory( AppDbContext context, IUnitOfWork unitOfWork )
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<Like> AddLikeAsync( int plogId, int userId )
        {
            var like = new Like
            {
                IsLiked = true,
                PlogId = plogId,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Likes.AddAsync( like );
            await _unitOfWork.SaveChangesAsync();
            return like;

        }

        public async Task<Like> GetLikeById( int Id )
        {
            var like = await _context.Likes
                .FirstOrDefaultAsync( l => l.Id == Id );

            if (like == null)
            {
                throw new KeyNotFoundException( $"Like with ID {Id} not found." );
            }
            return like;
        }

        public async Task<Like> GetLikeByPlogIdAndUserIdAsync( int plogId, int userId )
        {
            var like = await _context.Likes
                .FirstOrDefaultAsync( l => l.PlogId == plogId && l.UserId == userId );

            if (like == null)
            {
                throw new KeyNotFoundException( $"Like for Plog ID {plogId} and User ID {userId} not found." );
            }

            return like;
        }

        public async Task<string> RemoveLikeAsync( int plogId, int userId )
        {
            var like = await GetLikeByPlogIdAndUserIdAsync( plogId, userId );
            if (like == null)
            {
                throw new KeyNotFoundException( $"Like for Plog ID {plogId} and User ID {userId} not found." );
            }

            _context.Likes.Remove( like );
            await _unitOfWork.SaveChangesAsync();
            return $"Like for Plog ID {plogId} and User ID {userId} has been removed successfully.";
        }


    }
}
