using Blog_API.DTOs;
using Blog_API.Models;
using Blog_API.Repoistries.Base;
using Blog_API.Services.Base;

namespace Blog_API.Services
{
    public class AccountService : IAccountService
    {

        // Dependencies
        private readonly IMainRepoistory<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;


        // Constructor
        public AccountService( IMainRepoistory<User> userRepository, IUnitOfWork unitOfWork )
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        // Get All Users
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();

            // Check if there are no users
            if (!users.Any())
                return Enumerable.Empty<UserDto>();

            // Map User entities to UserDto
            var userDtos = users.Select( user => new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            } );

            return userDtos;
        }

        // Get User By Id
        public async Task<UserDto> GetUserByIdAsync( int userId )
        {
            var user = await _userRepository.GetByIdAsync( userId );

            // Check if user exists
            if (user == null)
                throw new KeyNotFoundException( $"User with ID {userId} not found." );


            // Map User entity to UserDto
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };
        }

        // Register a new user
        public async Task<UserDto> RegisterAsync( UserDto dto )
        {
            // Validate input
            if (string.IsNullOrWhiteSpace( dto.Name ) || string.IsNullOrWhiteSpace( dto.Email ) || string.IsNullOrWhiteSpace( dto.Password ))
                throw new ArgumentException( "Name, Email, and Password are required." );

            // Check if the email already exists
            var existingUser = await _userRepository.GetByConditionAsync( u => u.Email == dto.Email );
            if (existingUser != null)
                throw new InvalidOperationException( "A user with this email already exists." );


            // Create a new User entity
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword( dto.Password ), // Hash the password
                Role = dto.Role
            };

            // Save the user to the repository
            await _userRepository.AddAsync( user );
            await _unitOfWork.SaveChangesAsync();

            // Return the user DTO without the password
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };

        }

        // Login a user
        public async Task<UserDto> LoginAsync( LoginDto  dto)
        {

            // Validate input
            var user = await _userRepository.GetByConditionAsync( u => u.Email == dto.Email );

            // Check if the user is found and verify the password
            if (user == null || !BCrypt.Net.BCrypt.Verify( dto.Password, user.PasswordHash ))
                throw new UnauthorizedAccessException( "Invalid email or password." );

           
            // Map User entity to UserDto
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                Password = null // Do not return the password
            };
        }



        public async Task<string> DeleteAccountAsync( int userId )
        {
            var user = await _userRepository.GetByIdAsync( userId );

            // Check if user exists
            if (user == null)
                throw new KeyNotFoundException( $"User with ID {userId} not found." );

            // Delete the user
            await _userRepository.DeleteAsync( userId );
            await _unitOfWork.SaveChangesAsync();

            return $"User with ID {userId} has been deleted successfully.";
        }



    }
}
