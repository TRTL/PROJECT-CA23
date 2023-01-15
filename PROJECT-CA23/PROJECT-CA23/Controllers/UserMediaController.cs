using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PROJECT_CA23.Models;
using PROJECT_CA23.Models.Dto.MediaDtos;
using PROJECT_CA23.Models.Dto.UserMediaDtos;
using PROJECT_CA23.Repositories.IRepositories;
using PROJECT_CA23.Services.Adapters;
using PROJECT_CA23.Services.Adapters.IAdapters;
using System.Net.Mime;

namespace PROJECT_CA23.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserMediaController : ControllerBase
    {
        private readonly IUserMediaRepository _userMediaRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMediaRepository _mediaRepo;
        private readonly IUserMediaAdapter _userMediaAdapter;
        private readonly ILogger<UserMediaController> _logger;

        public UserMediaController(IUserMediaRepository userMediaRepo,
                                   IUserRepository userRepo,
                                   IMediaRepository mediaRepo,
                                   IUserMediaAdapter userMediaAdapter,
                                   ILogger<UserMediaController> logger)
        {
            _userMediaRepo = userMediaRepo;
            _userRepo = userRepo;
            _mediaRepo = mediaRepo;
            _userMediaAdapter = userMediaAdapter;
            _logger = logger;
        }


        /// <summary>
        /// Get list of all medias
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Client could not authenticate a request</response>
        /// <response code="500">Internal server error</response>
        [Authorize(Roles = "admin,user")]
        [HttpGet("/GetAllUserMedias")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserMediaDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAllUserMedias([FromBody] GetUserMediaRequest req)
        {
            _logger.LogInformation("GetAllUserMedias atempt with data: {req}", JsonConvert.SerializeObject(req));
            try
            {
                var allMedia = await _userMediaRepo.GetAllAsync(filter: um => um.UserId == req.UserId, includeTables: new List<string>() { "Medias" });
                var userMediaDtoList = allMedia.Select(userMedia => _userMediaAdapter.Bind(userMedia)).ToList();
                return Ok(userMediaDtoList);
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
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Client could not authenticate a request</response>
        /// <response code="500">Internal server error</response>
        [Authorize(Roles = "admin,user")]
        [HttpPost("/AddUserMedia")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserMediaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> AddUserMedia([FromBody] AddUserMediaRequest req)
        {
            _logger.LogInformation("AddUserMedia atempt with data: {req}", JsonConvert.SerializeObject(req));
            try
            {
                if (req.UserId == 0 || req.MediaId == 0)
                {
                    _logger.LogInformation($"{DateTime.Now} AddUserMedia required fields are invalid.");
                    return BadRequest("Required fields are invalid");
                }

                var user = _userRepo.Get(req.UserId);
                if (user == null)
                {
                    _logger.LogInformation($"{DateTime.Now} AddUserMedia. User with userId {req.UserId} was not found");
                    return NotFound($"User with userId {req.UserId} was not found");
                }

                var media = await _mediaRepo.GetAsync(m => m.MediaId == req.MediaId);
                if (media == null)
                {
                    _logger.LogInformation($"{DateTime.Now} AddUserMedia. Media with mediaId {req.MediaId} was not found");
                    return NotFound($"Media with mediaId {req.MediaId} was not found");
                }

                var newUserMedia = _userMediaAdapter.Bind(user, media);
                await _userMediaRepo.CreateAsync(newUserMedia);
                return Created("", newUserMedia);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now} AddUserMedia exception error.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


    }
}
