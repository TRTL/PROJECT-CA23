using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROJECT_CA23.Models
{
    public class Media
    {
        public int MediaId { get; set; }
        public string? Type { get; set; }

        [MaxLength(1000, ErrorMessage = "Title text cannot be longer than 1000 symbols")]
        public string Title { get; set; }

        [MaxLength(9, ErrorMessage = "Year text cannot be longer than 9 symbols")]
        public string? Year { get; set; }

        [MaxLength(30, ErrorMessage = "Runtime text cannot be longer than 30 symbols")]
        public string? Runtime { get; set; }
        public string? Director { get; set; }
        public string? Writer { get; set; }
        public string? Actors { get; set; }

        [MaxLength(2000, ErrorMessage = "Plot text cannot be longer than 2000 symbols")]
        public string? Plot { get; set; }
        public string? Language { get; set; }
        public string? Country { get; set; }
        public string? Poster { get; set; }
        public string? imdbId { get; set; }

        [Range(0.0, 10.0, ErrorMessage = "ImdbRating must be between 0.0 and 10.0")]
        public double? imdbRating { get; set; } = null;

        [Range(0, 10_000_000_000, ErrorMessage = "ImdbVotes must be between 0 and 10,000,000,000")]
        public decimal? imdbVotes { get; set; } = null;
        public virtual List<Genre> Genres { get; set; } = new List<Genre>();
        public virtual List<Review> Reviews { get; set; } = new List<Review>();         



    }
}
