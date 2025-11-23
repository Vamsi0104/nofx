using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AuthorId { get; set; }
    }
}
