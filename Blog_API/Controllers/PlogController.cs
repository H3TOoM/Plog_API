using Blog_API.DTOs;
using Blog_API.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog_API.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class PlogController : ControllerBase
    {
        // Inject the Plog service
        private readonly IPlogService _plogService;
        public PlogController( IPlogService plogService )
        {
            _plogService = plogService;
        }


        // Get all plogs
        [HttpGet]
        public async Task<IActionResult> GetAllPlogs()
        {
            try
            {
                var plogs = await _plogService.GetAllPlogsAsync();
                return Ok( plogs );
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound( ex.Message );
            }
            catch (Exception ex)
            {
                return StatusCode( StatusCodes.Status500InternalServerError, ex.Message );
            }
        }




        // Get plog by ID
        [HttpGet( "{id:int}" )]
        public async Task<IActionResult> GetPlogById( int id )
        {
            try
            {
                var plog = await _plogService.GetPlogByIdAsync( id );
                return Ok( plog );
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound( ex.Message );
            }
            catch (Exception ex)
            {
                return StatusCode( StatusCodes.Status500InternalServerError, ex.Message );
            }
        }


        // Add a new plog
        [HttpPost]
        public async Task<IActionResult> AddPlog( [FromBody] PlogDto dto )
        {
            if (dto == null)
            {
                return BadRequest( "Plog data cannot be null." );
            }
            try
            {
                var plog = await _plogService.AddPlogAsync( dto );
                return CreatedAtAction( nameof( GetPlogById ), new { id = plog.Id }, plog );
            }
            catch (Exception ex)
            {
                return StatusCode( StatusCodes.Status500InternalServerError, ex.Message );
            }
        }


        // Update an existing plog
        [HttpPut( "{id:int}" )]
        public async Task<IActionResult> UpdatePlog( int id, [FromBody] PlogDto dto )
        {
            if (dto == null)
            {
                return BadRequest( "Plog data cannot be null." );
            }
            try
            {
                var updatedPlog = await _plogService.UpdatePlogAsync( id, dto );
                return Ok( updatedPlog );
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound( ex.Message );
            }
            catch (Exception ex)
            {
                return StatusCode( StatusCodes.Status500InternalServerError, ex.Message );
            }
        }


        // Delete a plog
        [HttpDelete( "{id:int}" )]
        public async Task<IActionResult> DeletePlog( int id )
        {
            try
            {
                await _plogService.DeletePlogAsync( id );
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound( ex.Message );
            }
            catch (Exception ex)
            {
                return StatusCode( StatusCodes.Status500InternalServerError, ex.Message );
            }
        }





    }
}
