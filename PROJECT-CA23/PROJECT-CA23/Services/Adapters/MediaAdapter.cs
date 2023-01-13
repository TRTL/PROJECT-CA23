using PROJECT_CA23.Controllers;
using PROJECT_CA23.Models;
using PROJECT_CA23.Models.Dto.MediaDtos;
using PROJECT_CA23.Models.Dto.GenreDtos;
using PROJECT_CA23.Repositories.IRepositories;
using PROJECT_CA23.Services.Adapters.IAdapters;
using System.Diagnostics.Metrics;
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

        public MediaDto Bind(Media media)
        {
            return new MediaDto()
            {
                MediaId = media.MediaId,
                Type  = media.Type,
                Title = media.Title,
                Year = media.Year,
                Runtime = media.Runtime,
                Director = media.Director,
                Writer = media.Writer,
                Actors = media.Actors,
                Plot = media.Plot,
                Language = media.Language,
                Country = media.Country,
                Poster = media.Poster,
                imdbId = media.imdbId,
                imdbRating = media.imdbRating,
                Genres = media.Genres.Select(g => new GenreDto(g)).ToList()
            };
        }

    }
}
