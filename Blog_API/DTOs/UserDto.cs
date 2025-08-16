namespace Blog_API.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } // e.g., "Admin", "User", "Author"

        public string Password { get; set; } 
    }
}
