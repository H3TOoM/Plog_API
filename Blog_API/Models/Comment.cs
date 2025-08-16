using System.ComponentModel.DataAnnotations;

namespace Blog_API.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(1000, ErrorMessage = "Content cannot be longer than 1000 characters.")]
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } 
        public int PlogId { get; set; }
        public Plog Plog { get; set; } 
    }
}
