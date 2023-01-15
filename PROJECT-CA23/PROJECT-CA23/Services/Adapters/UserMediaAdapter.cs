using PROJECT_CA23.Models.Dto.GenreDtos;
using PROJECT_CA23.Models.Dto.MediaDtos;
using PROJECT_CA23.Models;
using PROJECT_CA23.Services.Adapters.IAdapters;
using PROJECT_CA23.Models.Dto.UserMediaDtos;
using PROJECT_CA23.Models.Enums;

namespace PROJECT_CA23.Services.Adapters
{
    public class UserMediaAdapter : IUserMediaAdapter
    {
        public UserMediaDto Bind(UserMedia userMedia)
        {
            EUserRating? nullabe_UserRating = null;
            string? nullabe_ReviewText = null;

            if (userMedia.Review != null)
            {
                nullabe_UserRating = userMedia.Review.UserRating;
                nullabe_ReviewText = userMedia.Review.ReviewText;
            }

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
                UserMediaStatus = userMedia.UserMediaStatus,
                Note = userMedia.Note,
                ReviewId = userMedia.ReviewId ?? null,
                UserRating = nullabe_UserRating,
                ReviewText = nullabe_ReviewText
            };
        }

        public UserMedia Bind(User user, Media media)
        {
            return new UserMedia()
            {
                UserId= user.UserId,
                User = user,
                MediaId = media.MediaId,
                Media = media,
                Note = null,
                ReviewId = null,
                Review = null
            };
        }
    }
}
