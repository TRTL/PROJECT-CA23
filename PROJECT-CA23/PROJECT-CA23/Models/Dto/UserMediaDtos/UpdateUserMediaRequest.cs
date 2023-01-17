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
        /// User Media Status. Available options are: "Wishlist", "Watching", "Finished".
        /// </summary>
        [Required]
        public string UserMediaStatus { get; set; }

        /// <summary>
        /// User Rating. Available options are: "NoRating", "OneStar", "TwoStars", "ThreeStars", "FourStars", "FiveStars".
        /// </summary>
        public string? UserRating { get; set; }

        /// <summary>
        /// Review Text for the media
        /// </summary>
        [MaxLength(1000, ErrorMessage = "Review text cannot be longer than 1000 characters")]
        public string? ReviewText { get; set; }

        /// <summary>
        /// Personal user note
        /// </summary>
        [MaxLength(1000, ErrorMessage = "Note cannot be longer than 1000 characters")]
        public string? Note { get; set; }
    }
}
