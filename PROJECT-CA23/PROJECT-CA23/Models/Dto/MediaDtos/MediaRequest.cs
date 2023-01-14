using System.ComponentModel.DataAnnotations;

namespace PROJECT_CA23.Models.Dto.MediaDtos
{
    public class MediaRequest
    {
        /// <summary>
        /// Select a type for new media. Passible types: movie or series
        /// </summary>
        [Required]
        [MaxLength(30, ErrorMessage = "Type text cannot be longer than 30 symbols")]
        public string Type { get; set; }

        /// <summary>
        /// Title of new media
        /// </summary>
        [Required]
        [MaxLength(1000, ErrorMessage = "Title text cannot be longer than 1000 symbols")]
        public string Title { get; set; }

        /// <summary>
        /// Year of release or runyears. Examples: "2008–2013" / "2018–"
        /// </summary>
        [MaxLength(9, ErrorMessage = "Year text cannot be longer than 9 symbols")]
        public string? Year { get; set; }

        /// <summary>
        /// Runtime in minutes
        /// </summary>
        [MaxLength(30, ErrorMessage = "Runtime text cannot be longer than 30 symbols")]
        public string? Runtime { get; set; }

        /// <summary>
        /// Directors
        /// </summary>
        public string? Director { get; set; }

        /// <summary>
        /// Writers
        /// </summary>
        public string? Writer { get; set; }

        /// <summary>
        /// Actors
        /// </summary>
        public string? Actors { get; set; }

        /// <summary>
        /// Movie or tv show summarized plot.
        /// </summary>
        [MaxLength(2000, ErrorMessage = "Plot text cannot be longer than 2000 symbols")]
        public string? Plot { get; set; }

        /// <summary>
        /// Languages
        /// </summary>
        public string? Language { get; set; }

        /// <summary>
        /// Country of release
        /// </summary>
        public string? Country { get; set; }

        /// <summary>
        /// Poster URL
        /// </summary>
        public string? Poster { get; set; }

        /// <summary>
        /// IMDB id
        /// </summary>
        public string? imdbId { get; set; }
    }
}
