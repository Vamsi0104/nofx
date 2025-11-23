using BlogApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IPostRepository Posts { get; }
        public ICommentRepository Comments { get; }

        public UnitOfWork(IPostRepository postRepo, ICommentRepository commentRepo)
        {
            Posts = postRepo;
            Comments = commentRepo;
        }

        public Task SaveChangesAsync()
        {
            // Not needed for file-based storage, return completed task
            return Task.CompletedTask;
        }
    }
}
