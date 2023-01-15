using PROJECT_CA23.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROJECT_CA23.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int MediaId { get; set; }
        public virtual Media Media { get; set; }
        public virtual UserMedia UserMedia { get; set; }
        public EUserRating UserRating { get; set; }

        [MaxLength(1000)]
        public string? ReviewText { get; set; }
    }
}
