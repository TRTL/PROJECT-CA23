using PROJECT_CA23.Models;
using PROJECT_CA23.Models.Dto.UserDtos;
using PROJECT_CA23.Services.Adapters.IAdapters;

namespace PROJECT_CA23.Services.Adapters
{
    public class UserAdapter : IUserAdapter
    {
        public User Bind(User user, UpdateUserDto dto)
        {
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            return user;
        }

        public UserDto Bind(User user)
        {
            return new UserDto
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Role = user.Role.ToString(),
                Created = user.Created,
                Updated = user.Updated
            };
        }
    }
}
