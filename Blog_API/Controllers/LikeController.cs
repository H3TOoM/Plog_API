using Blog_API.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog_API.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class LikeController : ControllerBase
    {
        // Inject the service
        private readonly ILikeService _likeService;
        public LikeController( ILikeService likeService )
        {
            _likeService = likeService;
        }


        // Add a like to a plog
        [HttpPost( "{plogId:int}/{userId:int}" )]
        public async Task<IActionResult> AddLike( int plogId, int userId )
        {
            // validate the plogId and userId
            if ( plogId <= 0 || userId <= 0 )
            {
                return BadRequest( new { message = "Invalid Plog ID or User ID." } );
            }


            await _likeService.AddLikeAsync( plogId, userId );
            return Ok( new { message = "Like added successfully." } );
        }

        // Get like by ID
        [HttpGet( "{id:int}" )]
        public async Task<IActionResult> GetLikeById( int id )
        {
            try
            {
                var like = await _likeService.GetLikeById( id );
                return Ok( like );
            }
            catch ( KeyNotFoundException knfEx )
            {
                return NotFound( new { message = knfEx.Message } );
            }
            catch ( Exception ex )
            {
                return StatusCode( StatusCodes.Status500InternalServerError, ex.Message );
            }
        }


        // Get like by Plog ID and User ID
        [HttpGet( "plog/{plogId:int}/user/{userId:int}" )]
        public async Task<IActionResult> GetLikeByPlogIdAndUserId( int plogId, int userId )
        {
            try
            {
                var like = await _likeService.GetLikeByPlogIdAndUserIdAsync( plogId, userId );
                return Ok( like );
            }
            catch ( KeyNotFoundException knfEx )
            {
                return NotFound( new { message = knfEx.Message } );
            }
            catch ( Exception ex )
            {
                return StatusCode( StatusCodes.Status500InternalServerError, ex.Message );
            }
        }

        // Remove a like from a plog
        [HttpDelete( "{plogId:int}/{userId:int}" )]
        public async Task<IActionResult> RemoveLike( int plogId, int userId )
        {
            // validate the plogId and userId
            if ( plogId <= 0 || userId <= 0 )
            {
                return BadRequest( new { message = "Invalid Plog ID or User ID." } );
            }
            try
            {
                var message = await _likeService.RemoveLikeAsync( plogId, userId );
                return Ok( new { message } );
            }
            catch ( KeyNotFoundException knfEx )
            {
                return NotFound( new { message = knfEx.Message } );
            }
            catch ( Exception ex )
            {
                return StatusCode( StatusCodes.Status500InternalServerError, ex.Message );
            }
        }


    }
}
