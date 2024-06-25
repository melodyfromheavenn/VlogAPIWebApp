using System.ComponentModel;

namespace VlogAPIWebApp.Models
{
    public class Friend
    {
        public int FriendId { get; set; }

  
        [DisplayName("Користувач")]
        public int UserId { get; set; }

        public User User { get; set; }

 
        [DisplayName("Друг")]
        public int FriendUserId { get; set; }

        public User FriendUser { get; set; }
    }
}
