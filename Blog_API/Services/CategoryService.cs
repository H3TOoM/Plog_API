using Blog_API.DTOs;
using Blog_API.Models;
using Blog_API.Repoistries.Base;
using Blog_API.Services.Base;

namespace Blog_API.Services
{
    public class CategoryService : ICategoryService
    {
        // Inject Repository and unit of work
        private readonly IUnitOfWork _unitOfWork;
        
        private readonly IMainRepoistory<Category> _categoryRepository;


        // inject Category repository 
        private readonly ICategoryRepoistory _categoryRepo;

        public CategoryService(IUnitOfWork unitOfWork, IMainRepoistory<Category> categoryRepository, ICategoryRepoistory categoryRepo )
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _categoryRepo = categoryRepo;
        }



        public async Task<IEnumerable<CategoryResDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
           
            if (categories == null || !categories.Any())
            {
                throw new KeyNotFoundException("No categories found.");
            }

            return categories.Select(c => new CategoryResDto
            {
                Id = c.Id,
                Name = c.Name,
                Plogs = c.Plogs?.Select(p => new PlogDto
                {
                    Title = p.Title,
                    Content = p.Content
                }).ToList() ?? new List<PlogDto>()
            }).ToList();
        }

        // Get all categories with plogs
        public async Task<IEnumerable<CategoryResDto>> GetAllCategoriesWithPlogsAsync()
        {
            var categories = await _categoryRepo.GetAllCategoriesWithPlogsAsync();
            if (categories == null || !categories.Any())
            {
                throw new KeyNotFoundException("No categories found with plogs.");
            }
            return categories;
        }


        // get category by id
        public async Task<CategoryResDto> GetCategoryByIdAsync( int id )
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with id {id} not found.");
            }

            return new CategoryResDto
            {
                Id = category.Id,
                Name = category.Name,
                Plogs = category.Plogs.Select(p => new PlogDto
                {
                    Title = p.Title,
                    Content = p.Content
                }).ToList()
            };
        }

        // Add a new category
        public async Task<CategoryResDto> AddCategoryAsync( CategoryReqDto dto )
        {
            var category = new Category
            {
                Name = dto.Name,
            };

            await _categoryRepository.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();

            

            return new CategoryResDto
            {
                Id = category.Id,
                Name = category.Name,
                Plogs = category.Plogs?.Select(p => new PlogDto
                {
                    Title = p.Title,
                    Content = p.Content
                }).ToList() ?? new List<PlogDto>()
            };




        }


        // Update an existing category
        public async Task<CategoryResDto> UpdateCategoryAsync( int id, CategoryReqDto dto )
        {
            var existingCategory =await _categoryRepository.GetByIdAsync(id);
            if (existingCategory == null)
            {
                throw new KeyNotFoundException($"Category with id {id} not found.");
            }

            existingCategory.Name = dto.Name;
            await _categoryRepository.UpdateAsync(id, existingCategory);
            await _unitOfWork.SaveChangesAsync();

            return new CategoryResDto
            {
                Id = existingCategory.Id,
                Name = existingCategory.Name,
                Plogs = existingCategory.Plogs?.Select(p => new PlogDto
                {
                    Title = p.Title,
                    Content = p.Content
                }).ToList() ?? new List<PlogDto>()
            };
        }



        // Delete a category
        public async Task<string> DeleteCategoryAsync( int id )
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with id {id} not found.");
            }

             await _categoryRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return "Deleted Successfully!";

        }



    }
}
