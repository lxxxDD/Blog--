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
    public class UsersController : ControllerBase
    {
        private readonly DreamBlogContext _context;

        public UsersController(DreamBlogContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        //登录
        [HttpPost]
        public async Task<ActionResult<IEnumerable<User>>> Login( User userLoginDto)
        {
          var user= await _context.Users.FirstOrDefaultAsync(u=>u.Email== userLoginDto.Email&&u.Password== userLoginDto.Password);

            if (user==null)
            {
                return Ok("error");
            }
            return Ok(user);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<ActionResult<User>> PutUser(User userInfo)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userInfo.UserId);
            if (user==null)
            {
                return BadRequest();
            }
        

            user.Password = userInfo.Password;
      
            user.Username = userInfo.Username;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               
            }

            return Ok(user);
        }


        [HttpPut]
        
        public async Task<ActionResult<IEnumerable<User>>> updateUserImg(User userImg)
        {
         var user =await _context.Users.FirstOrDefaultAsync(x => x.UserId == userImg.UserId);
            if (user == null)
            {
                return Ok(user);

            }
            user.ImgUrl = userImg.ImgUrl;
           await _context.SaveChangesAsync();
            return Ok(user);
        }

        // POST: api/Users
        //注册
        [HttpPost]
        public async Task<ActionResult<User>> Register(User user)
        {
       
          var isUserName=await _context.Users.FirstOrDefaultAsync(u=>u.Username == user.Username);

            var isEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (isUserName != null)
            {
                //用户名已注册
                return Ok("1");
            }
            if (isEmail != null)
            {
                //邮箱已注册
                return Ok("2");
            }
            // 选择随机头像
            string[] randomAvatars = { "uploadImg\\SImg\\1.png", "uploadImg\\SImg\\2.png", "uploadImg\\SImg\\3.png" };
            Random random = new Random();
            int index = random.Next(randomAvatars.Length);
            string selectedAvatar = randomAvatars[index];

            // 分配头像路径给用户
            user.ImgUrl = selectedAvatar;

   
            user.CreatedAt = DateTime.Now;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
