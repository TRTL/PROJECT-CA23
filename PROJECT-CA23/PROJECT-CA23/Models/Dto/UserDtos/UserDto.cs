namespace PROJECT_CA23.Models.Dto.UserDtos
{
    public class UserDto
    {
        /// <summary>
        /// Id of user
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// First name of user
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of user
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Unique username of user
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Role assigned for user
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Date and time user was created
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Last date and time user data was edited
        /// </summary>
        public DateTime Updated { get; set; }

    }
}
