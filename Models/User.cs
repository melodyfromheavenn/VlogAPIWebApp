using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace VlogAPIWebApp.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Поле ПІБ є обов'язковим")]
        [DisplayName("ПІБ")]
        [StringLength(100, ErrorMessage = "ПІБ не може перевищувати 100 символів")]
        public string FullName { get; set; }

        [DisplayName("Місто")]
        [StringLength(50, ErrorMessage = "Місто не може перевищувати 50 символів")]
        public string City { get; set; }

        [DisplayName("Статус")]
        [StringLength(250, ErrorMessage = "Статус не може перевищувати 250 символів")]
        public string Status { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<FriendRequest> SentFriendRequests { get; set; }
        public ICollection<FriendRequest> ReceivedFriendRequests { get; set; }
        public ICollection<Friend> Friends { get; set; }
    }
}
