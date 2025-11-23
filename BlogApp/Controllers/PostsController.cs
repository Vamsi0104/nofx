using BlogApp.Domain.Entities;
using BlogApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaginatedPosts([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var posts = await _unitOfWork.Posts.GetPaginatedPostsAsync(page, pageSize);
            return Ok(posts);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] Post post)
        {
            post.CreatedAt = DateTime.UtcNow;
            post.IsDeleted = false;
            await _unitOfWork.Posts.AddAsync(post);
            await _unitOfWork.SaveChangesAsync();
            return Ok(post);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _unitOfWork.Posts.GetByIdAsync(id);
            if (post == null)
                return NotFound();

            _unitOfWork.Posts.SoftDelete(post);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("{postId}/comments")]
        public async Task<IActionResult> AddComment(int postId, [FromBody] Comment comment)
        {
            var post = await _unitOfWork.Posts.GetByIdAsync(postId);
            if (post == null)
                return NotFound();

            comment.PostId = postId;
            comment.CreatedAt = DateTime.UtcNow;
            await _unitOfWork.Comments.AddAsync(comment);
            await _unitOfWork.SaveChangesAsync();
            return Ok(comment);
        }

        [HttpGet("{postId}/comments")]
        public async Task<IActionResult> GetComments(int postId)
        {
            var comments = await _unitOfWork.Comments.GetByPostIdAsync(postId);
            return Ok(comments);
        }
    }
}
