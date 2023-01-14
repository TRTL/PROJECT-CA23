using PROJECT_CA23.Models.Api;

namespace PROJECT_CA23.Services.IServices
{
    public interface IMediaService
    {
        Task<OmdbApiMedia?> SearchForMediaAtOmdbApiAsync(string mediaTitle);
    }
}
