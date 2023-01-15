namespace PROJECT_CA23.Models.Dto.UserMediaDtos
{
    public class UserMediaRequest
    {

        /// <summary>
        /// User Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Optional filter: "Want to watch", "Watching" or "Finished"
        /// </summary>
        public string? Filter { get; set; }
    }
}
