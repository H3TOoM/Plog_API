using Blog_API.Models;
using Blog_API.Repoistries.Base;
using Blog_API.Services.Base;

namespace Blog_API.Services
{
    public class LikeService : ILikeService
    {
        // inject ILikeRepoistory
        private readonly ILikeRepoistory _likeRepoistory;
        private readonly IUnitOfWork _unitOfWork;

        public LikeService( ILikeRepoistory likeRepoistory, IUnitOfWork unitOfWork )
        {
            _likeRepoistory = likeRepoistory;
            _unitOfWork = unitOfWork;
        }


        // Add a like to a plog
        public async Task<Like> AddLikeAsync( int plogId, int userId )
        {
            var like = await _likeRepoistory.AddLikeAsync( plogId, userId );
            await _unitOfWork.SaveChangesAsync();

            return like;

        }


        // Get like by ID
        public async Task<Like> GetLikeById( int Id )
        {
            var like = await _likeRepoistory.GetLikeById( Id );
            if (like == null)
            {
                throw new KeyNotFoundException( $"Like with ID {Id} not found." );
            }
            return like;
        }


        // Get like by Plog ID and User ID
        public async Task<Like> GetLikeByPlogIdAndUserIdAsync( int plogId, int userId )
        {
            var like = await _likeRepoistory.GetLikeByPlogIdAndUserIdAsync( plogId, userId );
            if (like == null)
            {
                throw new KeyNotFoundException( $"Like for Plog ID {plogId} and User ID {userId} not found." );
            }
            return like;
        }


        // Remove a like from a plog
        public async Task<string> RemoveLikeAsync( int plogId, int userId )
        {
            var like = await _likeRepoistory.GetLikeByPlogIdAndUserIdAsync( plogId, userId );
            if (like == null)
            {
                throw new KeyNotFoundException( $"Like for Plog ID {plogId} and User ID {userId} not found." );
            }
            await _likeRepoistory.RemoveLikeAsync( plogId, userId );
            await _unitOfWork.SaveChangesAsync();
            return "Like removed successfully.";
        }
    }
}
