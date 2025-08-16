using Blog_API.DTOs;

namespace Blog_API.Services.Base
{
    public interface ICategoryService
    {
      
        Task<IEnumerable<CategoryResDto>> GetAllCategoriesAsync();

        Task<IEnumerable<CategoryResDto>> GetAllCategoriesWithPlogsAsync();
        Task<CategoryResDto> GetCategoryByIdAsync(int id);

        Task<CategoryResDto> AddCategoryAsync(CategoryReqDto dto);

        Task<CategoryResDto> UpdateCategoryAsync(int id, CategoryReqDto dto);

        Task<string> DeleteCategoryAsync(int id);


    }
}
