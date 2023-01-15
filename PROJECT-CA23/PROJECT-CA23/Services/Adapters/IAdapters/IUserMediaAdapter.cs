using PROJECT_CA23.Models;
using PROJECT_CA23.Models.Dto.UserMediaDtos;

namespace PROJECT_CA23.Services.Adapters.IAdapters
{
    public interface IUserMediaAdapter
    {
        UserMediaDto Bind(UserMedia userMedia);
        UserMedia Bind(User user, Media media);
    }
}
