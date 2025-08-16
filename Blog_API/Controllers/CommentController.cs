using Blog_API.DTOs;
using Blog_API.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog_API.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class CommentController : ControllerBase
    {
        // Inject the service
        private readonly ICommentService _commentService;
        public CommentController( ICommentService commentService )
        {
            _commentService = commentService;
        }

        // Get All Comments

        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            try
            {
                var comments = await _commentService.GetAllCommentsAsync();
                return Ok( comments );
            }
            catch (Exception ex)
            {
                return StatusCode( StatusCodes.Status500InternalServerError, ex.Message );
            }
        }

        // Get Commentby ID

        [HttpGet( "{id:int}" )]
        public async Task<IActionResult> GetCommentById( int id )
        {
            try
            {
                var comment = await _commentService.GetCommentByIdAsync( id );
                return Ok( comment );
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


        // Get Comments by Plog ID
        [HttpGet( "plog/{plogId:int}" )]
        public async Task<IActionResult> GetCommentsByPlogId( int plogId )
        {
            try
            {
                var comments = await _commentService.GetCommentsByPlogIdAsync( plogId );
                return Ok( comments );
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

        // Get Comments by User ID
        [HttpGet( "user/{userId:int}" )]
        public async Task<IActionResult> GetCommentsByUserId( int userId )
        {
            try
            {
                var comments = await _commentService.GetCommentsByUserIdAsync( userId );
                return Ok( comments );
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


        // Add a new comment
        [HttpPost]
        public async Task<IActionResult> AddComment( [FromBody] CommentDto commentDto )
        {
            if (commentDto == null)
            {
                return BadRequest( "Comment data cannot be null." );
            }
            try
            {
                var createdComment = await _commentService.AddCommentAsync( commentDto );
                return CreatedAtAction( nameof( GetCommentById ), new { id = createdComment.Id }, createdComment );
            }
            catch (Exception ex)
            {
                return StatusCode( StatusCodes.Status500InternalServerError, ex.Message );
            }
        }

        // Update a comment
        [HttpPut( "{id:int}" )]
        public async Task<IActionResult> UpdateComment( int id, [FromBody] CommentDto commentDto )
        {
            if (commentDto == null)
            {
                return BadRequest( "Comment data cannot be null." );
            }
            try
            {
                var updatedComment = await _commentService.UpdateCommentAsync( id, commentDto );
                return Ok( updatedComment );
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


        // Delete a comment
        [HttpDelete( "{id:int}" )]
        public async Task<IActionResult> DeleteComment( int id )
        {
            try
            {
                await _commentService.DeleteCommentAsync( id );
                return NoContent();
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
