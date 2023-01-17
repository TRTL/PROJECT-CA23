using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PROJECT_CA23.Models.Api;
using PROJECT_CA23.Models;
using System.Net.Mime;
using PROJECT_CA23.Repositories.IRepositories;
using PROJECT_CA23.Services.Adapters.IAdapters;
using PROJECT_CA23.Services.IServices;
using Microsoft.IdentityModel.Tokens;
using PROJECT_CA23.Repositories.RepositoryServices.IRepositoryServices;

namespace PROJECT_CA23.Controllers
{
    /// <summary>
    /// Hi mom!
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class OmdbController : ControllerBase
    {
        private readonly IMediaRepository _mediaRepo;
        private readonly ILogger<OmdbController> _logger;
        private readonly IMediaAdapter _mediaAdapter;
        private readonly IMediaService _mediaService;
        private readonly IGenreRepoService _genreRepoService;

        public OmdbController(IMediaRepository mediaRepo,
                              ILogger<OmdbController> logger,
                              IMediaAdapter mediaAdapter,
                              IMediaService mediaService,
                              IGenreRepoService genreRepoService)
        {
            _mediaRepo = mediaRepo;
            _logger = logger;
            _mediaAdapter = mediaAdapter;
            _mediaService = mediaService;
            _genreRepoService = genreRepoService;
        }



        /// <summary>
        /// Search for media at Omdb Api by title
        /// </summary>
        /// <param name="mediaTitle">title to search for</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Client could not authenticate a request</response>
        /// <response code="500">Internal server error</response>
        [Authorize(Roles = "admin")]
        [HttpGet("/SearchForMediaAtOmdb")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OmdbApiMedia))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> SearchForMediaAtOmdb([FromQuery] string mediaTitle)
        {
            _logger.LogInformation($"SearchForMediaAtOmdb atempt with mediaTitle: {mediaTitle}");

            try
            {
                var omdbApiMedia = await _mediaService.SearchForMediaAtOmdbApiAsync(mediaTitle);
                return Ok(omdbApiMedia);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now} SearchForMediaAtOmdb exception error.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Add media from Omdb Api
        /// </summary>
        /// <param name="req">Media fields. Type and title are required</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Client could not authenticate a request</response>
        /// <response code="500">Internal server error</response>
        [Authorize(Roles = "admin")]
        [HttpPost("/AddMediaFromOmdb")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OmdbApiMedia))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> AddMediaFromOmdb([FromBody] OmdbApiMedia req)
        {
            _logger.LogInformation("AddMediaFromOmdb atempt with data: {req}", JsonConvert.SerializeObject(req));
            try
            {
                if (req.Type.IsNullOrEmpty() || req.Title.IsNullOrEmpty())
                {
                    _logger.LogInformation($"{DateTime.Now} AddMediaFromOmdb required fields are missing.");
                    return BadRequest("Type and title are required fields");
                }

                if (await _mediaRepo.ExistAsync(m => m.Title== req.Title)) return BadRequest("Media with same title already exists in DB.");

                List<Genre>? mediaGenres = await _genreRepoService.AddNewAndGetExistingGenres(req.Genre);

                var newMedia = _mediaAdapter.Bind(req, mediaGenres);

                await _mediaRepo.CreateAsync(newMedia);
                return CreatedAtRoute("GetMediaById", new { id = newMedia.MediaId }, newMedia);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now} AddMediaFromOmdb exception error.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
