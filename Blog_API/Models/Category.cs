using System.ComponentModel.DataAnnotations;

namespace Blog_API.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }
        public ICollection<Plog>? Plogs { get; set; } 

    }
}
