using BlogApp.Domain.Entities;

namespace BlogApp.Domain.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetByPostIdAsync(int postId);
        Task AddAsync(Comment comment);
    }
}
