using Blog_API.Models;

namespace Blog_API.Services.Base
{
    public interface ILikeService
    {
        public Task<string> RemoveLikeAsync(int plogId, int userId);
        public Task<Like> AddLikeAsync(int plogId, int userId);
        public Task<Like> GetLikeById(int Id);
        public Task<Like> GetLikeByPlogIdAndUserIdAsync(int plogId, int userId);
   
    }
}
