using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blog2.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]

    public class CommitController : ControllerBase
    {

        private readonly DreamBlogContext _context;
        public CommitController(DreamBlogContext context)
        {
            _context = context;
        }
        // GET: api/Users
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        //{
        //    return await _context.Users.ToListAsync();
        //}
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommit()
        {
            return await _context.Comments.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(Comment comment)
        {
            comment.CreatedAt = DateTime.Now;
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return Ok(comment);
        }
        [HttpGet]
        public async Task<ActionResult<Comment>> GetComments(int id)
        {
          var comm= await _context.Comments.Include(u=>u.User).Where(u=>u.PostId==id).OrderByDescending(u=>u.CreatedAt).ToListAsync();

            return Ok(comm);
        }


    }
}
