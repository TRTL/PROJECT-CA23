using System.ComponentModel.DataAnnotations;

namespace PROJECT_CA23.Models.Dto.LoginDtos
{
    public class LoginRequest
    {
        /// <summary>
        /// User's username for login
        /// </summary>
        [MaxLength(100, ErrorMessage = "Username cannot be longer than 100 characters")]
        public string Username { get; set; }

        /// <summary>
        /// User's password
        /// </summary>
        [MaxLength(100, ErrorMessage = "Password cannot be longer than 100 characters")]
        public string Password { get; set; }
    }
}
