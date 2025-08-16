using Blog_API.DTOs;

namespace Blog_API.Repoistries.Base
{
    public interface ICategoryRepoistory
    {
        Task<IEnumerable<CategoryResDto>> GetAllCategoriesWithPlogsAsync();
      
    }
}
