using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PROJECT_CA23.Models;
using PROJECT_CA23.Models.Api;
using PROJECT_CA23.Models.Dto.AddressDto;
using PROJECT_CA23.Models.Dto.AddressDtos;
using PROJECT_CA23.Models.Dto.MediaDtos;
using PROJECT_CA23.Repositories;
using PROJECT_CA23.Repositories.IRepositories;
using PROJECT_CA23.Services.Adapters;
using PROJECT_CA23.Services.Adapters.IAdapters;
using PROJECT_CA23.Services.IServices;
using System.Net.Mime;
using System.Security.Claims;

namespace PROJECT_CA23.Controllers
{
    /// <summary>
    /// Read, add or delete of media 
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly IMediaRepository _mediaRepo;
        private readonly ILogger<MediaController> _logger;
        private readonly IMediaAdapter _mediaAdapter;
        private readonly IReviewRepository _reviewRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MediaController(IMediaRepository mediaRepo,
                               ILogger<MediaController> logger,
                               IMediaAdapter mediaAdapter,
                               IReviewRepository reviewRepo,
                               IHttpContextAccessor httpContextAccessor)
        {
            _mediaRepo = mediaRepo;
            _logger = logger;
            _mediaAdapter = mediaAdapter;
            _reviewRepo = reviewRepo;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Get media by mediaId
        /// </summary>
        /// <param name="id">Media Id</param>
        /// <returns></returns>
        /// <response code="200">Indicates that the request has succeeded</response>
        /// <response code="400">Server cannot or will not process the request</response>
        /// <response code="401">Client request has not been completed because it lacks valid authentication credentials for the requested resource</response>
        /// <response code="500">Server encountered an unexpected condition that prevented it from fulfilling the request</response>
        [Authorize(Roles = "admin,user")]
        [HttpGet("/GetMediaById/{id:int}", Name = "GetMediaById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Media>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetMediaById(int id)
        {
            _logger.LogInformation($"GetMediaById atempt with MediaId - {id}");
            try
            {
                var currentUserRole = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
                var currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
                if (currentUserRole != "admin" && currentUserId != id)
                {
                    _logger.LogWarning($"{DateTime.Now} user {currentUserId} tried to access user {id} data");
                    return Forbid("You are not authorized to acces requested data");
                }

                if (id <= 0)
                {
                    _logger.LogInformation($"{DateTime.Now} Failed GetMediaById attempt for MediaId - {id}. MediaId is incorrect.");
                    return BadRequest("MediaId is incorrect.");
                }

                var mediaExist = await _mediaRepo.ExistAsync(m => m.MediaId == id);
                if (!mediaExist)
                {
                    _logger.LogInformation($"{DateTime.Now} Failed GetMediaById atempt with MediaId - {id}. MediaId does not exists.");
                    return NotFound("MediaId does not exists");
                }

                var media = await _mediaRepo.GetAsync(m => m.MediaId == id, new List<string>() { "Genres" });
                var reviews = await _reviewRepo.GetAllAsync(m => m.MediaId == id, new List<string>() { "User" });
                media = _mediaAdapter.Bind(media, reviews);

                return Ok(media);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now} GetAllMediasByGenreId exception error.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }


        /// <summary>
        /// Get list of all medias
        /// </summary>
        /// <param name="type">Media type: movie or series</param>
        /// <returns></returns>
        /// <response code="200">Indicates that the request has succeeded</response>
        /// <response code="400">Server cannot or will not process the request</response>
        /// <response code="401">Client request has not been completed because it lacks valid authentication credentials for the requested resource</response>
        /// <response code="500">Server encountered an unexpected condition that prevented it from fulfilling the request</response>
        [Authorize(Roles = "admin,user")]
        [HttpGet("/GetAllMedias")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MediaDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAllMedias([FromQuery] string type)
        {
            _logger.LogInformation($"GetAllMedias atempt with filter type - {type}");
            try
            {
                if (type != "movie" && type != "series")
                {
                    _logger.LogInformation($"{DateTime.Now} GetAllMedias filter type is invalid.");
                    return BadRequest("Media type is invalid");
                }

                var allMedia = await _mediaRepo.GetAllAsync(filter: m => m.Type == type, includeTables: new List<string>() { "Genres" }, orderByColumn: o => o.Title);
                var mediaDtoList = allMedia.Select(m => _mediaAdapter.Bind(m)).ToList();
                return Ok(mediaDtoList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now} GetAllMedias exception error.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Add media by entering fields of media
        /// </summary>
        /// <param name="req">Media fields. Type and title are required</param>
        /// <returns></returns>
        /// <response code="201">Indicates that the request has succeeded and has led to the creation of a resource</response>
        /// <response code="400">Server cannot or will not process the request</response>
        /// <response code="401">Client request has not been completed because it lacks valid authentication credentials for the requested resource</response>
        /// <response code="500">Server encountered an unexpected condition that prevented it from fulfilling the request</response>
        [Authorize(Roles = "admin")]
        [HttpPost("/AddMediaManually")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Media))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> AddMediaManually([FromBody] MediaRequest req)
        {
            _logger.LogInformation("AddMediaManually atempt with data: {req}", JsonConvert.SerializeObject(req));
            try
            {
                if (req.Type.IsNullOrEmpty() || req.Title.IsNullOrEmpty())
                {
                    _logger.LogInformation($"{DateTime.Now} AddMediaManually required fields are missing.");
                    return BadRequest("Type and title are required fields");
                }

                var newMedia = _mediaAdapter.Bind(req);
                await _mediaRepo.CreateAsync(newMedia);
                return CreatedAtRoute("GetMediaById", new { id = newMedia.MediaId }, newMedia);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now} AddMediaManually exception error.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Deleting Media by mediaId
        /// </summary>
        /// <param name="id">Media Id</param>
        /// <returns></returns>
        /// <response code="204">Server has successfully fulfilled the request and there is no content returned</response>
        /// <response code="400">Server cannot or will not process the request</response>
        /// <response code="401">Client request has not been completed because it lacks valid authentication credentials for the requested resource</response>
        /// <response code="404">Server cannot find the requested resource</response>
        /// <response code="500">Server encountered an unexpected condition that prevented it from fulfilling the request</response>
        [Authorize(Roles = "admin")]
        [HttpDelete("/DeleteMedia/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMedia(int id)
        {
            _logger.LogInformation($"DeleteMedia atempt with id - {id}");
            try
            {
                if (id <= 0)
                {
                    _logger.LogInformation("DeleteMedia request id - {id} is incorrect", id);
                    return BadRequest("DeleteMedia request id is incorrect.");
                }

                var mediaExist = await _mediaRepo.ExistAsync(m => m.MediaId == id);
                if (!mediaExist)
                {
                    _logger.LogInformation("DeleteMedia request with MediaId - {id} not found", id);
                    return NotFound("Media not found");
                }

                var entity = await _mediaRepo.GetAsync(m => m.MediaId == id);
                await _mediaRepo.RemoveAsync(entity);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now} DeleteMedia exception error.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
