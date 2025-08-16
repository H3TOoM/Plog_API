namespace Blog_API.DTOs
{
    public class CategoryResDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
      
        public List<PlogDto>? Plogs { get; set; } = new List<PlogDto>();
    }
}
