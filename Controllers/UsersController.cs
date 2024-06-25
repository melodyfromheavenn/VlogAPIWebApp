using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VlogAPIWebApp.Models;

namespace VlogAPIWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly VlogAPIContext _context;

        public UsersController(VlogAPIContext context)
        {
            _context = context;
        }

        [HttpGet("{id}/Friends")]
        public async Task<ActionResult<IEnumerable<User>>> GetUserFriends(int id)
        {
            var user = await _context.Users
                .Include(u => u.Friends)
                .ThenInclude(f => f.FriendUser)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            return user.Friends.Select(f => f.FriendUser).ToList();
        }
        [HttpPost("{id}/FriendRequests")]
        public async Task<ActionResult<FriendRequest>> SendFriendRequest(int id, FriendRequest friendRequest)
        {
            if (id != friendRequest.SenderId)
            {
                return BadRequest();
            }

            _context.FriendRequests.Add(friendRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFriendRequest", new { id = friendRequest.FriendRequestId }, friendRequest);
        }
        [HttpGet("{id}/FriendRequests")]
        public async Task<ActionResult<IEnumerable<FriendRequest>>> GetUserFriendRequests(int id)
        {
            var friendRequests = await _context.FriendRequests
                .Where(fr => fr.ReceiverId == id && !fr.IsAccepted)
                .ToListAsync();

            return friendRequests;
        }
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            // Перевірка наявності порожніх колекцій
            user.Posts = user.Posts ?? new List<Post>();
            user.Friends = user.Friends ?? new List<Friend>();
            user.SentFriendRequests = user.SentFriendRequests ?? new List<FriendRequest>();
            user.ReceivedFriendRequests = user.ReceivedFriendRequests ?? new List<FriendRequest>();

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
