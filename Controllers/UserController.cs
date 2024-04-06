using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserApi.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.WebUtilities;
namespace gpcalanderbackenddotnet.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;
        private readonly MD5 md5 = MD5.Create();
        //https://learn.microsoft.com/en-us/dotnet/core/extensions/logging?tabs=command-line
        ILogger logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger("gpcalanderbackenddotnet-UserController");

        public UserController(UserContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(long id)
        {
            return _context.User.Any(e => e.Id == id);
        }

        [HttpPost]
        [Route("~/logincheck")] 
        public async Task<ActionResult<User>> logincheck([FromForm]String username, [FromForm]String password)
        {

            //var user =  await _context.User.Where(user=>user.Username == username).FirstOrDefault();
            var user = await _context.User.Where(user=>user.Username == username).FirstOrDefaultAsync();
         
            if ( user == null)
            {
                return NotFound();
            }
            String checkingString = user.Password + user.Seckey;
            logger.LogInformation(checkingString);
            byte[] checkingStringByte =  md5.ComputeHash(checkingString.Select(s=>Convert.ToByte(s)).ToArray());

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < checkingStringByte.Length; i++)
            {
                sb.Append(checkingStringByte[i].ToString("x2")); // 将字节转换为十六进制字符串
            }
            logger.LogInformation(sb.ToString());
            logger.LogInformation(password);
            if(password.Equals(sb.ToString())){
                logger.LogInformation("password check pass");
                return user;
            }else{
                logger.LogInformation("password not match");
                return NotFound();
            }
        }
        [HttpPost]
        [Route("~/getSecKey")] 
        public async  Task<ActionResult<string>> getSecKey([FromForm] string username){
           //string username  = Request.Form["username"];
           //https://code-maze.com/aspnetcore-pass-parameters-to-http-get-action/
            
            var user = await _context.User.Where(user=>user.Username == username).FirstOrDefaultAsync();
            return user.Seckey;
        }

    
       
    }
    
}
