using System.ComponentModel.DataAnnotations;

namespace Blog_API.Models
{
    public class Like
    {
        [Key]
        public int Id { get; set; }


        public bool IsLiked { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }

        [Required]
        public int PlogId { get; set; }
        public Plog Plog { get; set; }
    }
}
