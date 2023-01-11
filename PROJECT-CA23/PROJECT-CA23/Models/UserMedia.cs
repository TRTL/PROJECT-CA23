using PROJECT_CA23.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROJECT_CA23.Models
{
    public class UserMedia
    {
        [Required(ErrorMessage = "UserId is required")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Required(ErrorMessage = "MediaId is required")]
        public int MediaId { get; set; }
        public virtual Media Media { get; set; }
        public EMediaStatus MediaStatus { get; set; } = EMediaStatus.Wishlist;

        [MaxLength(1000, ErrorMessage = "Note text cannot be longer than 1000 symbols")]
        public string? Note { get; set; }
    }
}
