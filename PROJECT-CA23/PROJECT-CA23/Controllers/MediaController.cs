using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PROJECT_CA23.Models;
using PROJECT_CA23.Models.Dto.AddressDto;
using PROJECT_CA23.Models.Dto.AddressDtos;
using PROJECT_CA23.Models.Dto.MediaDtos;
using PROJECT_CA23.Repositories;
using PROJECT_CA23.Repositories.IRepositories;
using PROJECT_CA23.Services.Adapters.IAdapters;
using System.Security.Claims;

namespace PROJECT_CA23.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly IMediaRepository _mediaRepo;
        private readonly ILogger<MediaController> _logger;
        private readonly IMediaAdapter _mediaAdapter;

        public MediaController(IMediaRepository mediaRepo, 
                               ILogger<MediaController> logger, 
                               IMediaAdapter mediaAdapter)
        {
            _mediaRepo = mediaRepo;
            _logger = logger;
            _mediaAdapter = mediaAdapter;
        }

        /// <summary>
        /// Get media by mediaId
        /// </summary>
        /// <param name="id">Media Id</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Client could not authenticate a request</response>
        /// <response code="500">Internal server error</response>
        [Authorize(Roles = "admin,user")]
        [HttpGet("/GetMediaById/{id:int}", Name = "GetMediaById")]
        [ProducesResponseType(StatusCodes.Status200OK)] // , Type = typeof(AddressDto)
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMediaById(int id)
        {
            _logger.LogInformation($"GetMediaById atempt with MediaId - {id}");
            try
            {
                var mediaExist = await _mediaRepo.ExistAsync(m => m.MediaId == id);
                if (!mediaExist)
                {
                    _logger.LogInformation($"{DateTime.Now} Failed GetMediaById atempt with MediaId - {id}. MediaId does not exists.");
                    return NotFound("MediaId does not exists");
                }
                var media = _mediaRepo.GetAsync(m => m.MediaId == id, new List<string>() { "Genres" }).Result;
                return Ok(media);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now} GetAllMediasByGenreId exception error.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        /// <summary>
        /// Add media by mediaId
        /// </summary>
        /// <param name="id">Media Id</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Client could not authenticate a request</response>
        /// <response code="500">Internal server error</response>
        [Authorize(Roles = "admin,user")]
        [HttpPost("/AddMedia")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Media))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> AddMedia([FromBody] MediaRequest req)
        {
            _logger.LogInformation("AddMedia atempt with data: {req}", JsonConvert.SerializeObject(req));

            try
            {
                var newMedia = _mediaAdapter.Bind(req);
                await _mediaRepo.CreateAsync(newMedia);
                return CreatedAtRoute("GetMediaById", new { id = newMedia.MediaId }, newMedia);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now} AddMedia exception error.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
