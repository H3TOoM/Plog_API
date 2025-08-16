using System.ComponentModel.DataAnnotations;

namespace Blog_API.Models
{
    public class Plog
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "Title cannot be longer than 200 characters.")]
        public string Title { get; set; }

        [Required]
        [StringLength(5000, ErrorMessage = "Content cannot be longer than 5000 characters.")]   
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

       
        public int UserId { get; set; }
        public User User { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }


        public ICollection<Comment> Comments { get; set; }   
        public ICollection<Like> Likes { get; set; } 
    }
}
