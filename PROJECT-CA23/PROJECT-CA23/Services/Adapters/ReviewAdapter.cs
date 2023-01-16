using PROJECT_CA23.Models;
using PROJECT_CA23.Models.Enums;
using PROJECT_CA23.Services.Adapters.IAdapters;

namespace PROJECT_CA23.Services.Adapters
{
    public class ReviewAdapter : IReviewAdapter
    {
        public Review Bind(User user, UserMedia userMedia)
        {
            return new Review()
            {
                User = user,
                UserId= user.UserId,
                UserMedia = userMedia,
                UserMediaId= userMedia.UserMediaId,
                Media = userMedia.Media,
                MediaId = userMedia.MediaId,
            };
        }
    }
}
