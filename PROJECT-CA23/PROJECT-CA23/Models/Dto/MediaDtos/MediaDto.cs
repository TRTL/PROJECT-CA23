using PROJECT_CA23.Models.Dto.GenreDtos;
using System.ComponentModel.DataAnnotations;

namespace PROJECT_CA23.Models.Dto.MediaDtos
{
    public class MediaDto
    {
        /// <summary>
        /// Media Id
        /// </summary>
        public int MediaId { get; set; }

        /// <summary>
        /// Passible types: movie or series
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// Title of media
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Year of release or runyears. Examples: "2008–2013" / "2018–"
        /// </summary>
        public string? Year { get; set; }

        /// <summary>
        /// Runtime in minutes
        /// </summary>
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

        /// <summary>
        /// IMDB rating
        /// </summary>
        public double? imdbRating { get; set; }

        /// <summary>
        /// List of genres
        /// </summary>
        public List<GenreDto> Genres { get; set; }
    }
}
