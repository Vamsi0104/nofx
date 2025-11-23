using BlogApp.Domain.Entities;
using BlogApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlogApp.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private const string FilePath = "Data/comments.json";

        public async Task<IEnumerable<Comment>> GetByPostIdAsync(int postId)
        {
            var comments = await ReadCommentsAsync();
            return comments.Where(c => c.PostId == postId);
        }

        public async Task AddAsync(Comment comment)
        {
            var comments = (await ReadCommentsAsync()).ToList();
            comment.Id = comments.Count > 0 ? comments.Max(c => c.Id) + 1 : 1;
            comments.Add(comment);
            await WriteCommentsAsync(comments);
        }

        private async Task<List<Comment>> ReadCommentsAsync()
        {
            if (!File.Exists(FilePath))
                return new List<Comment>();

            var json = await File.ReadAllTextAsync(FilePath);
            return JsonSerializer.Deserialize<List<Comment>>(json) ?? new List<Comment>();
        }
        private async Task WriteCommentsAsync(List<Comment> comments)
        {
            var directory = Path.GetDirectoryName(FilePath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory!);

            var json = JsonSerializer.Serialize(comments, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(FilePath, json);
        }
    }
}
