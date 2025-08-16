namespace Blog_API.DTOs
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int UserId { get; set; }

        public int PlogId { get; set; }
    }
}
