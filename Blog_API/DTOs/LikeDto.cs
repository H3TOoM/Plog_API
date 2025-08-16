namespace Blog_API.DTOs
{
    public class LikeDto
    {
        public bool IsLiked { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public int UserId { get; set; }

       
        public int PlogId { get; set; }

    }
}
