using PROJECT_CA23.Models.Enums;

namespace PROJECT_CA23.Models
{
    public class Review
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int MediaId { get; set; }
        public virtual Media Media { get; set; }
        public EUserRating UserRating { get; set; }
        public string? ReviewText { get; set; }
    }
}
