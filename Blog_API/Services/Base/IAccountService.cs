using Blog_API.DTOs;

namespace Blog_API.Services.Base
{
    public interface IAccountService
    {
        Task<UserDto> RegisterAsync(UserDto dto);
        Task<UserDto> LoginAsync(LoginDto dto);
        Task<string> DeleteAccountAsync(int userId);
        Task<UserDto> GetUserByIdAsync(int userId);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
    }
}
