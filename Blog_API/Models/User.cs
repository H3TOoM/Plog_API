using System.ComponentModel.DataAnnotations;

namespace Blog_API.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        public string PasswordHash { get; set; }
       
        [Required]
        [StringLength(50, ErrorMessage = "Role cannot be longer than 50 characters.")]
        public string Role { get; set; } // e.g., "Admin", "User" , "Author"

        public ICollection<Plog>? Plogs { get; set; } 
        public ICollection<Comment>? Comments { get; set; } 

        public ICollection<Like>? Likes { get; set; }
    }
}
