using PROJECT_CA23.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROJECT_CA23.Models
{
    public class Review
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int MediaId { get; set; }
        public virtual Media Media { get; set; }
        public EUserRating UserRating { get; set; }

        [MaxLength(1000, ErrorMessage = "Review text cannot be longer than 1000 symbols")]
        public string? ReviewText { get; set; }
    }
}
