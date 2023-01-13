using PROJECT_CA23.Models;
using PROJECT_CA23.Models.Dto.MediaDtos;

namespace PROJECT_CA23.Services.Adapters.IAdapters
{
    public interface IMediaAdapter
    {
        Media Bind(MediaRequest req);
        MediaDto Bind(Media media);
    }
}
