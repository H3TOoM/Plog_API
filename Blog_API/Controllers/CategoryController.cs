using Blog_API.DTOs;
using Blog_API.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog_API.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    [Authorize( Roles = "Author,Admin" )]
    public class CategoryController : ControllerBase
    {
        // Inject the service
        private readonly ICategoryService _categoryService;
        public CategoryController( ICategoryService categoryService )
        {
            _categoryService = categoryService;
        }


        // Get all categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                return Ok( categories );
            }
            catch (Exception ex)
            {
                return StatusCode( StatusCodes.Status500InternalServerError, ex.Message );
            }
        }

        // Get all categories with plogs
        [HttpGet( "with-plogs" )]
        public async Task<IActionResult> GetAllCategoriesWithPlogs()
        {
            try
            {
                var categories = await _categoryService.GetAllCategoriesWithPlogsAsync();
                return Ok( categories );
            }
            catch (Exception ex)
            {
                return StatusCode( StatusCodes.Status500InternalServerError, ex.Message );
            }
        }


        // Get category by ID
        [HttpGet( "{id:int}" )]
        public async Task<IActionResult> GetCategoryById( int id )
        {
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync( id );
                return Ok( category );
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound( knfEx.Message );
            }
            catch (Exception ex)
            {
                return StatusCode( StatusCodes.Status500InternalServerError, ex.Message );
            }
        }


        // Add a new category
        [HttpPost]
        public async Task<IActionResult> AddCategory( [FromBody] CategoryReqDto dto )
        {
            if (dto == null)
            {
                return BadRequest( "Category data cannot be null." );
            }
            try
            {
                var category = await _categoryService.AddCategoryAsync( dto );
                return CreatedAtAction( nameof( GetCategoryById ), new { id = category.Id }, category );
            }
            catch (Exception ex)
            {
                return StatusCode( StatusCodes.Status500InternalServerError, ex.Message );
            }
        }


        // Update an existing category
        [HttpPut( "{id:int}" )]
        public async Task<IActionResult> UpdateCategory( int id, [FromBody] CategoryReqDto dto )
        {
            if (dto == null)
            {
                return BadRequest( "Category data cannot be null." );
            }
            try
            {
                var updatedCategory = await _categoryService.UpdateCategoryAsync( id, dto );
                return Ok( updatedCategory );
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound( knfEx.Message );
            }
            catch (Exception ex)
            {
                return StatusCode( StatusCodes.Status500InternalServerError, ex.Message );
            }
        }


        // Delete a category
        [HttpDelete( "{id:int}" )]
        public async Task<IActionResult> DeleteCategory( int id )
        {
            try
            {
                var result = await _categoryService.DeleteCategoryAsync( id );
                return Ok( result );
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound( knfEx.Message );
            }
            catch (Exception ex)
            {
                return StatusCode( StatusCodes.Status500InternalServerError, ex.Message );
            }



        }
    }
}
