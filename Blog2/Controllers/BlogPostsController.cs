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
        [HttpGet]
        public async Task<ActionResult<BlogPost>>  GetBlogPost(int id)
        {
            var currentBlogPost = await _context.BlogPosts.FindAsync(id);

            if (currentBlogPost == null)
            {
                return NotFound();
            }
            // 获取其他文章，并按照发布日期进行排序
            var orderedBlogPosts = await _context.BlogPosts.Include(u=>u.User)
                .OrderByDescending(post => post.CreatedAt) // 按照发布日期降序排序
                .ToListAsync();

            // 将当前文章移动到排序结果的第一个位置
            orderedBlogPosts.Remove(currentBlogPost); // 先从排序结果中移除当前文章
            orderedBlogPosts.Insert(0, currentBlogPost); // 再将当前文章插入到排序结果的第一个位置
            return Ok(orderedBlogPosts);
        }

//获取当前用户的所有文章
        [HttpGet]
        public async Task<ActionResult<BlogPost>> GetUserBlogPostlist(int uid,int sort=0)
        {
            var blogPostsQuery = _context.BlogPosts.Include(u => u.User).Where(u => u.UserId == uid);

            if (sort == 1) {
                blogPostsQuery = blogPostsQuery.OrderByDescending(u=>u.Views);

            }else if (sort == 2)
            {
                //日期的格式怎么排序
                blogPostsQuery = blogPostsQuery.OrderByDescending(u => u.CreatedAt);
        
            }
            var blogPosts = await blogPostsQuery.ToListAsync();
            if (blogPosts == null)
            {
                return NotFound();
            }

            return Ok(blogPosts);
        }
        // GET: 获取当前用户的所有文章汇总？？
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


        //点赞接口
        [HttpPut]
        public async Task<IActionResult> setLike(int postId)
        {
            // 根据文章ID查找对应的文章
            var blogPost = await _context.BlogPosts.FindAsync(postId);

            if (blogPost == null)
            {
                return NotFound(); // 如果找不到文章，返回404 Not Found
            }

            // 假设您的点赞逻辑是简单地在原有的点赞数上加1
            blogPost.Likes += 1;

            // 更新数据库中的点赞数
            _context.BlogPosts.Update(blogPost);
            await _context.SaveChangesAsync();

            // 返回点赞成功的消息给客户端
            return Ok("点赞成功~");
        }

        [HttpPut]
        public async Task<IActionResult> setView(int postId)
        {
            // 根据文章ID查找对应的文章
            var blogPost = await _context.BlogPosts.FindAsync(postId);

            if (blogPost == null)
            {
                return NotFound(); // 如果找不到文章，返回404 Not Found
            }

            // 假设您的点赞逻辑是简单地在原有的点赞数上加1
            blogPost.Views += 1;

            // 更新数据库中的点赞数
            _context.BlogPosts.Update(blogPost);
            await _context.SaveChangesAsync();

            // 返回点赞成功的消息给客户端
            return Ok("浏览1");
        }
        // POST: api/BlogPosts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

    //发布
        [HttpPost]
        public async Task<ActionResult<BlogPost>> PostBlogPost(BlogPost blogPost)
        {
            blogPost.CreatedAt = DateTime.Now;
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
