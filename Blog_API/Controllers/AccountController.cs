using Blog_API.DTOs;
using Blog_API.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog_API.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class AccountController : ControllerBase
    {
        // Dependencies
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;
        // Constructor
        public AccountController( IAccountService accountService, ITokenService tokenService )
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }


        // Get All Users
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _accountService.GetAllUsersAsync();
            if (users == null || !users.Any())
            {
                return NotFound( "No users found." );
            }
            return Ok( users );
        }

        // Get User By Id
        [HttpGet( "{Id:int}" )]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetUserById( int Id )
        {
            try
            {
                var user = await _accountService.GetUserByIdAsync( Id );
                return Ok( user );
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound( ex.Message );
            }
        }

        // Register User
        [HttpPost( "register" )]
        public async Task<IActionResult> Register( [FromBody] UserDto userDto )
        {
            if (userDto == null)
            {
                return BadRequest( "User data is required." );
            }
            try
            {
                var registeredUser = await _accountService.RegisterAsync( userDto );
                if (registeredUser == null)
                {
                    return BadRequest( "User registration failed." );
                }
                // Generate JWT token
                var token = await _tokenService.CreateTokenAsync( registeredUser );
                // Return user data and token
                return Ok( new { Token = token, User = registeredUser } );
            }
            catch (Exception ex)
            {
                return BadRequest( ex.Message );
            }
        }


        // Login User
        [HttpPost( "login" )]
        public async Task<IActionResult> Login( [FromBody] LoginDto loginDto )
        {
            if (loginDto == null || string.IsNullOrEmpty( loginDto.Email ) || string.IsNullOrEmpty( loginDto.Password ))
            {
                return BadRequest( "Email and password are required." );
            }
            try
            {
                var user = await _accountService.LoginAsync( loginDto );
                if (user == null)
                {
                    return Unauthorized( "Invalid email or password." );
                }

                // Generate JWT token
                var token = await _tokenService.CreateTokenAsync( user );
                // Return user data and token
                return Ok( new { Token = token, User = user } );
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized( ex.Message );
            }
            catch (Exception ex)
            {
                return BadRequest( ex.Message );
            }
        }


        // Delete User
        [Authorize]
        [HttpDelete( "{Id:int}" )]
        public async Task<IActionResult> DeleteUser( int Id )
        {
            try
            {
                var result = await _accountService.DeleteAccountAsync( Id );
                return Ok( result );
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound( ex.Message );
            }
            catch (Exception ex)
            {
                return BadRequest( ex.Message );
            }

        }
    }
}
