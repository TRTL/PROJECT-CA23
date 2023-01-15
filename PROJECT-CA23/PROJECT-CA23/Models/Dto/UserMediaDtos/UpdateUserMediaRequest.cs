using PROJECT_CA23.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PROJECT_CA23.Models.Dto.UserMediaDtos
{
    public class UpdateUserMediaRequest
    {

        /// <summary>
        /// User media Id to update
        /// </summary>
        public int? UserMediaId { get; set; }

        /// <summary>
        /// UserMediaStatus
        /// </summary>
        [Required]
        public string UserMediaStatus { get; set; }

        /// <summary>
        /// User Rating
        /// </summary>
        public string? UserRating { get; set; }

        /// <summary>
        /// Review Text
        /// </summary>
        public string? ReviewText { get; set; }

        /// <summary>
        /// Note
        /// </summary>
        public string? Note { get; set; }
    }
}
