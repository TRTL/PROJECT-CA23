using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROJECT_CA23.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }

        [Required(ErrorMessage = "UserId is required")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [MaxLength(100, ErrorMessage = "Notification title text cannot be longer than 100 symbols")]
        public int Title { get; set; }

        [MaxLength(1000, ErrorMessage = "Notification text cannot be longer than 1000 symbols")]
        public int Text { get; set; }
        public bool Shown { get; set; } = false;
    }
}
