namespace PROJECT_CA23.Models.Dto.UserMediaDtos
{
    public class AddUserMediaRequest
    {
        /// <summary>
        /// User by user id to whom media will added
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Media Id of a media that will be added
        /// </summary>
        public int MediaId { get; set; }        
    }
}
