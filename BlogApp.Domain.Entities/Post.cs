using System.Xml.Linq;

namespace BlogApp.Domain.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<Comment> Comments { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public string AuthorId { get; set; }
        public bool IsDeleted { get; set; }
    }
}