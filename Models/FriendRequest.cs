using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace VlogAPIWebApp.Models
{
    public class FriendRequest
    {
        [Key]
        public int FriendRequestId { get; set; }

        [Required]
        [DisplayName("Відправник")]
        public int SenderId { get; set; }

        public User Sender { get; set; }

        [Required]
        [DisplayName("Отримувач")]
        public int ReceiverId { get; set; }

        public User Receiver { get; set; }

        [DisplayName("Прийнято")]
        public bool IsAccepted { get; set; }
    }
}
