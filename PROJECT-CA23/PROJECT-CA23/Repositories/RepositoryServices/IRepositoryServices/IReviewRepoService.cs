using PROJECT_CA23.Models;
using PROJECT_CA23.Models.Dto.UserMediaDtos;

namespace PROJECT_CA23.Repositories.RepositoryServices.IRepositoryServices
{
    public interface IReviewRepoService
    {
        Task<UserMedia> AddReviewIfNeeded(UserMedia userMedia, UpdateUserMediaRequest req);
    }
}
