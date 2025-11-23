using BlogApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Domain.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetPaginatedPostsAsync(int page, int pageSize);
        Task<Post?> GetByIdAsync(int id);
        Task AddAsync(Post post);
        void SoftDelete(Post post);
    }
}
