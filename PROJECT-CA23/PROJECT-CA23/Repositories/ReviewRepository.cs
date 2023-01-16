using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PROJECT_CA23.Database;
using PROJECT_CA23.Models;
using PROJECT_CA23.Models.Dto.UserMediaDtos;
using PROJECT_CA23.Repositories.IRepositories;

namespace PROJECT_CA23.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        private readonly CA23Context _db;

        public ReviewRepository(CA23Context db) : base(db)
        {
            _db = db;
        }

        public async Task<UserMedia> AddReviewIfNeeded(UserMedia userMedia, UpdateUserMediaRequest req)
        {
            if (userMedia.Review == null && (!req.UserRating.IsNullOrEmpty() || !req.ReviewText.IsNullOrEmpty()))
            {
                Review review = new Review();
                review.UserId = userMedia.UserId;
                review.User = userMedia.User;
                review.UserMediaId = userMedia.UserMediaId;
                review.UserMedia = userMedia;
                review.MediaId = userMedia.MediaId;

                _db.Add(review);
                await _db.SaveChangesAsync();

                userMedia.Review = review;
                userMedia.ReviewId = review.ReviewId;
            }
            return userMedia;
        }
    }
}
