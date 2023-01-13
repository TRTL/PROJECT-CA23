using PROJECT_CA23.Models;
using PROJECT_CA23.Models.Dto.MediaDtos;
using PROJECT_CA23.Services.Adapters.IAdapters;
using System.IO;

namespace PROJECT_CA23.Services.Adapters
{
    public class MediaAdapter : IMediaAdapter
    {
        public Media Bind(MediaRequest req)
        {
            return new Media()
            {
                Type  = req.Type,
                Title = req.Title,
                Year = req.Year,
                Runtime = req.Runtime,
                Director = req.Director,
                Writer = req.Writer,
                Actors = req.Actors,
                Plot = req.Plot,
                Language = req.Language,
                Country = req.Country,
                Poster = req.Poster,
                imdbId = req.imdbId
            };
        }
    }
}
