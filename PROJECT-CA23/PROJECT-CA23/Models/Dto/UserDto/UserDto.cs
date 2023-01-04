namespace PROJECT_CA23.Models.Dto.UserDto
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public DateTime LastLogin { get; set; }
        public bool IsDeleted { get; set; }

    }
}
