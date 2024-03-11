using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blog2.Models;

namespace Blog2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly DreamBlogContext _context;

        public BlogPostsController(DreamBlogContext context)
        {
            _context = context;
        }

        // GET: api/BlogPosts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogPost>>> GetBlogPosts(string title="", int addNum=0,int CID=0)
        {
            int num = 6;
            num += addNum;
            if(!string.IsNullOrEmpty(title)) {
                 
                return await _context.BlogPosts.Include(u=>u.User).Where(u => u.Title.Contains(title)).OrderByDescending(u=>u.Likes).ToListAsync();
            }
            var blogPosts = await _context.BlogPosts.Include(u => u.User).OrderByDescending(u => u.Likes).Take(num).ToListAsync();

            return blogPosts;
        }
        [HttpGet("total")]
        public async Task<ActionResult<int>> GetBlogPostsTotal()
        {
            var total = await _context.BlogPosts.CountAsync();

            return total;
        }
        // GET: api/BlogPosts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPost>> GetBlogPost(int id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);

            if (blogPost == null)
            {
                return NotFound();
            }

            return blogPost;
        }

//获取当前用户的所有文章
        [HttpGet]
        public async Task<ActionResult<BlogPost>> GetUserBlogPostlist(int uid)
        {
            var blogPost = await _context.BlogPosts.Include(u => u.User).Where(u=>u.UserId==uid).ToListAsync();

            if (blogPost == null)
            {
                return NotFound();
            }

            return Ok(blogPost);
        }
        // GET: 获取当前用户的所有文章
        [HttpGet]
        public async Task<ActionResult<BlogPost>> GetUserBlogPostLikeAndViews(int id)
        {
            try
            {
                // 从数据库或其他存储中获取当前用户的所有文章
                var userPosts = await _context.BlogPosts
                    .Where(post => post.UserId == id)
                    .ToListAsync();

                // 如果用户没有发布过文章，返回404错误
                if (userPosts == null || userPosts.Count == 0)
                {
                    return Ok("1");
                }

                // 计算总文章数
                int totalPosts = userPosts.Count;

                // 计算总查看数和总点赞数
                int totalViews = (int)userPosts.Sum(post => post.Views);
                int totalLikes = (int)userPosts.Sum(post => post.Likes);

                // 构造包含用户文章统计信息的对象
                var userBlogStats = new
                {
                    TotalPosts = totalPosts,
                    TotalViews = totalViews,
                    TotalLikes = totalLikes
                };

                // 返回用户文章统计信息
                return Ok(userBlogStats);
            }
            catch (Exception ex)
            {
                // 处理异常情况，返回500服务器内部错误
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }


        }

        // PUT: api/BlogPosts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogPost(int id, BlogPost blogPost)
        {
            if (id != blogPost.PostId)
            {
                return BadRequest();
            }

            _context.Entry(blogPost).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogPostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BlogPosts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BlogPost>> PostBlogPost(BlogPost blogPost)
        {
            _context.BlogPosts.Add(blogPost);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlogPost", new { id = blogPost.PostId }, blogPost);
        }

        // DELETE: api/BlogPosts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogPost(int id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }

            _context.BlogPosts.Remove(blogPost);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlogPostExists(int id)
        {
            return _context.BlogPosts.Any(e => e.PostId == id);
        }
    }
}
