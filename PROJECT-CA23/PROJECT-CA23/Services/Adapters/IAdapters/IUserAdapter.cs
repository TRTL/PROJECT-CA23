using PROJECT_CA23.Models;
using PROJECT_CA23.Models.Dto.UserDtos;

namespace PROJECT_CA23.Services.Adapters.IAdapters
{
    public interface IUserAdapter
    {
        User Bind(User user, UpdateUserDto dto);
        UserDto Bind(User user);
        IEnumerable<UserDto> Bind(IEnumerable<User> users);
    }
}
