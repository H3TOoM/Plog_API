using Blog_API.Models;

namespace Blog_API.Repoistries.Base
{
    public interface ILikeRepoistory
    {
        public Task<Like> AddLikeAsync(int plogId, int userId);
        public Task<string> RemoveLikeAsync(int plogId, int userId);

        public Task<Like> GetLikeById(int Id);  

        public Task<Like> GetLikeByPlogIdAndUserIdAsync(int plogId, int userId);
    }
}
