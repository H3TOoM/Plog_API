using Blog_API.Data;
using Blog_API.DTOs;
using Blog_API.Repoistries.Base;
using Microsoft.EntityFrameworkCore;

namespace Blog_API.Repoistries
{
    public class CategoryRepository : ICategoryRepoistory
    {

        // inject AppDbContext
        private readonly AppDbContext _context;
        public CategoryRepository( AppDbContext context )
        {
            _context = context;
        }

        // Get all categories with plogs
        public async Task<IEnumerable<CategoryResDto>> GetAllCategoriesWithPlogsAsync()
        {
            var categories = await _context.Categories
                .Include( c => c.Plogs )
                .Select( c => new CategoryResDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Plogs = c.Plogs.Select( p => new PlogDto
                    {
                        Title = p.Title,
                        Content = p.Content
                    } ).ToList()
                } ).ToListAsync();
            return categories;
        }
    }
}
