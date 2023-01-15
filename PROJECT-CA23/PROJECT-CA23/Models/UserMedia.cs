using PROJECT_CA23.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROJECT_CA23.Models
{
    public class UserMedia
    {
        public int UserMediaId { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        public int MediaId { get; set; }
        public virtual Media Media { get; set; }

        public EUserMediaStatus UserMediaStatus { get; set; } = EUserMediaStatus.Wishlist;

        [MaxLength(1000)]
        public string? Note { get; set; }

        public int? ReviewId { get; set; }
        public virtual Review? Review { get; set; }
    }
}
