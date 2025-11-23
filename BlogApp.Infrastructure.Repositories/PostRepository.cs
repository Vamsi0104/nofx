using BlogApp.Domain.Entities;
using BlogApp.Domain.Interfaces;
using System.Text.Json;

namespace BlogApp.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private const string FilePath = "Data/posts.json";

        public async Task<IEnumerable<Post>> GetPaginatedPostsAsync(int page, int pageSize)
        {
            var posts = await ReadPostsAsync();
            return posts
                .Where(p => !p.IsDeleted)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }

        public async Task<Post?> GetByIdAsync(int id)
        {
            var posts = await ReadPostsAsync();
            return posts.FirstOrDefault(p => p.Id == id && !p.IsDeleted);
        }

        public async Task AddAsync(Post post)
        {
            var posts = (await ReadPostsAsync()).ToList();
            post.Id = posts.Count > 0 ? posts.Max(p => p.Id) + 1 : 1;
            posts.Add(post);
            await WritePostsAsync(posts);
        }

        public async void SoftDelete(Post post)
        {
            post.IsDeleted = true;
            var posts = (await ReadPostsAsync()).ToList();
            var index = posts.FindIndex(p => p.Id == post.Id);
            if (index >= 0)
            {
                posts[index] = post;
                await WritePostsAsync(posts);
            }
        }

        private async Task<List<Post>> ReadPostsAsync()
        {
            if (!File.Exists(FilePath))
                return new List<Post>();

            var json = await File.ReadAllTextAsync(FilePath);
            return JsonSerializer.Deserialize<List<Post>>(json) ?? new List<Post>();
        }

        private async Task WritePostsAsync(List<Post> posts)
        {
            var directory = Path.GetDirectoryName(FilePath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory!);

            var json = JsonSerializer.Serialize(posts, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(FilePath, json);
        }

    }
}
