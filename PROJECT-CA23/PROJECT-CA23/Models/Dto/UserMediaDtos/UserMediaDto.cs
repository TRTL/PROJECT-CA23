namespace PROJECT_CA23.Models.Dto.UserMediaDtos
{
    public class UserMediaDto
    {
        /// <summary>
        /// User media Id
        /// </summary>
        public int UserMediaId { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Media Id
        /// </summary>
        public int MediaId { get; set; }

        /// <summary>
        /// Media type
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// Title of media
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Year of release or runyears.
        /// </summary>
        public string? Year { get; set; }

        /// <summary>
        /// IMDB id
        /// </summary>
        public string? imdbId { get; set; }

        /// <summary>
        /// IMDB rating
        /// </summary>
        public double? imdbRating { get; set; }

        /// <summary>
        /// Optional review Id
        /// </summary>
        public int? ReviewId { get; set; }
    }
}
