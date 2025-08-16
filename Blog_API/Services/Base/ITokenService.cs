using Blog_API.DTOs;

namespace Blog_API.Services.Base
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(UserDto user);
    }
}
