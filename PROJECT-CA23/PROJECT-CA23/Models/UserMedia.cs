using PROJECT_CA23.Models.Enums;

namespace PROJECT_CA23.Models
{
    public class UserMedia
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int MediaId { get; set; }
        public virtual Media Media { get; set; }
        public EMediaStatus MediaStatus { get; set; }
        public string? Note { get; set; }
    }
}
