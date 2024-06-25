using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace VlogAPIWebApp.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [Required(ErrorMessage = "Поле Зміст є обов'язковим")]
        [DisplayName("Зміст")]
        public string Content { get; set; }

        [DisplayName("Дата створення")]
        public DateTime CreatedAt { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
