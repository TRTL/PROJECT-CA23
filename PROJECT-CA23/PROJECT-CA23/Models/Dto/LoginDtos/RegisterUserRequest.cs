using System.ComponentModel.DataAnnotations;

namespace PROJECT_CA23.Models.Dto.LoginDtos
{
    public class RegisterUserRequest
    {
        /// <summary>
        /// First name of new user
        /// </summary>
        [MaxLength(200, ErrorMessage = "FirstName cannot be longer than 200 characters")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of new user
        /// </summary>
        [MaxLength(200, ErrorMessage = "LastName cannot be longer than 200 characters")]
        public string LastName { get; set; }

        /// <summary>
        /// Username for new user
        /// </summary>
        [MaxLength(100, ErrorMessage = "Username cannot be longer than 100 characters")]
        public string Username { get; set; }

        /// <summary>
        /// Password for new user
        /// </summary>
        [MaxLength(100, ErrorMessage = "Password cannot be longer than 100 characters")]
        public string Password { get; set; }

        /// <summary>
        /// Select a role for new user. Passible roles: admin or user
        /// </summary>
        [MaxLength(5, ErrorMessage = "Role cannot be longer than 5 characters")]
        public string Role { get; set; }
    }
}
