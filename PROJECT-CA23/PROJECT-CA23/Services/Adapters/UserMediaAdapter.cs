using PROJECT_CA23.Models.Dto.GenreDtos;
using PROJECT_CA23.Models.Dto.MediaDtos;
using PROJECT_CA23.Models;
using PROJECT_CA23.Services.Adapters.IAdapters;
using PROJECT_CA23.Models.Dto.UserMediaDtos;

namespace PROJECT_CA23.Services.Adapters
{
    public class UserMediaAdapter : IUserMediaAdapter
    {
        public UserMediaDto Bind(UserMedia userMedia)
        {
            return new UserMediaDto()
            {
                UserMediaId = userMedia.MediaId,
                UserId= userMedia.UserId,
                MediaId = userMedia.Media.MediaId,
                Type  = userMedia.Media.Type,
                Title = userMedia.Media.Title,
                Year = userMedia.Media.Year,
                imdbId = userMedia.Media.imdbId,
                imdbRating = userMedia.Media.imdbRating,
                ReviewId = userMedia.ReviewId ?? null
            };
        }

        public UserMedia Bind(User user, Media media)
        {
            return new UserMedia()
            {
                UserId= user.UserId,
                MediaId = media.MediaId,
                Note = null,
                ReviewId = null
            };
        }
    }
}
