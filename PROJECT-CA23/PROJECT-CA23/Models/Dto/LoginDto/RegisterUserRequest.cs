using System.ComponentModel.DataAnnotations;

namespace PROJECT_CA23.Models.Dto.LoginDto
{
    public class RegisterUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
