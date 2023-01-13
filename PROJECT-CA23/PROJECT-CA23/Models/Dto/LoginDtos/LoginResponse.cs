namespace PROJECT_CA23.Models.Dto.LoginDtos
{
    public class LoginResponse
    {
        /// <summary>
        /// Username of logged in user
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// JWT
        /// </summary>
        public string? Token { get; set; }
    }
}
