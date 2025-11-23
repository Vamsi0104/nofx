namespace BlogApp.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IPostRepository Posts { get; }
        ICommentRepository Comments { get; }
        Task SaveChangesAsync();
    }
}