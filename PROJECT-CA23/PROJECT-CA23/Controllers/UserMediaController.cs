using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PROJECT_CA23.Models;
using PROJECT_CA23.Models.Dto.AddressDtos;
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
        /// Get list of all specific user medias
        /// </summary>
        /// <param name="req">User id and filter.</param>
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
        public async Task<IActionResult> GetAllUserMedias([FromQuery] GetUserMediaRequest req)
        {
            _logger.LogInformation($"GetAllUserMedias atempt for userId: {req.UserId}");
            try
            {
                var allMedia = await _userMediaRepo.GetAllAsync(filter: um => um.UserId == req.UserId, includeTables: new List<string>() { "Media" });
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
        /// Add media to user media list
        /// </summary>
        /// <param name="req">Media id and user id. What and to whos list assign.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Client could not authenticate a request</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal server error</response>
        [Authorize(Roles = "admin,user")]
        [HttpPost("/AddUserMedia")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserMediaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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


        /// <summary>
        /// Update address by addressId
        /// </summary>
        /// <param name="req">Updatable fields</param>
        /// <returns></returns>
        /// <response code="204">Updated</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Client could not authenticate a request</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("/UpdateUserMedia")]
        [Authorize(Roles = "admin,user")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateUserMedia([FromBody] UpdateUserMediaRequest req)
        {
            _logger.LogInformation($"UpdateUserMedia atempt for UserMediaId - {req.UserMediaId}");
            try
            {
                if (req.UserMediaId <= 0)
                {
                    _logger.LogInformation($"{DateTime.Now} Failed UpdateUserMedia attempt for UserMediaId - {req.UserMediaId}. UpdateUserMedia request data is invalid.");
                    return BadRequest("UpdateUserMedia request data is invalid.");
                }

                if (!await _userMediaRepo.ExistAsync(m => m.UserMediaId == req.UserMediaId))
                {
                    _logger.LogInformation($"{DateTime.Now} Failed UpdateUserMedia attempt with AddressId - {req.UserMediaId}. UserMediaId not found.");
                    return NotFound("UserMediaId not found");
                }

                // PATIKRINTI AR REVIEW TURI? NEI NETURI O REVIEW DATA ATEJO, TAI REIKI SUKURTI NAUJ REVIEW IR JAM TA DATA PRISKIRTI!
                // PATIKRINTI AR REVIEW TURI? NEI NETURI O REVIEW DATA ATEJO, TAI REIKI SUKURTI NAUJ REVIEW IR JAM TA DATA PRISKIRTI!
                // PATIKRINTI AR REVIEW TURI? NEI NETURI O REVIEW DATA ATEJO, TAI REIKI SUKURTI NAUJ REVIEW IR JAM TA DATA PRISKIRTI!
                // PATIKRINTI AR REVIEW TURI? NEI NETURI O REVIEW DATA ATEJO, TAI REIKI SUKURTI NAUJ REVIEW IR JAM TA DATA PRISKIRTI!

                var userMedia = await _userMediaRepo.GetAsync(a => a.UserMediaId == req.UserMediaId, new List<string> { "Review" });
                userMedia = _userMediaAdapter.Bind(userMedia, req);
                await _userMediaRepo.UpdateAsync(userMedia);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now} UpdateUserMedia exception error.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
