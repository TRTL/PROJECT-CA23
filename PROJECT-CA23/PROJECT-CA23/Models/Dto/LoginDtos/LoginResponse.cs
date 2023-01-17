namespace PROJECT_CA23.Models.Dto.LoginDtos
{
    public class LoginResponse
    {
        /// <summary>
        /// Id of user that successfully logged in
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// JWT
        /// </summary>
        public string? Token { get; set; }
    }
}
