using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PROJECT_CA23.Models;
using PROJECT_CA23.Models.Dto.AddressDtos;
using PROJECT_CA23.Models.Dto.MediaDtos;
using PROJECT_CA23.Models.Dto.UserMediaDtos;
using PROJECT_CA23.Models.Enums;
using PROJECT_CA23.Repositories.IRepositories;
using PROJECT_CA23.Repositories.RepositoryServices;
using PROJECT_CA23.Repositories.RepositoryServices.IRepositoryServices;
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
        //private readonly IReviewRepoService _reviewRepoService;
        private readonly IReviewRepository _reviewRepo; // VIETOJE _reviewRepoService
        private readonly IUserMediaAdapter _userMediaAdapter;
        private readonly ILogger<UserMediaController> _logger;

        public UserMediaController(IUserMediaRepository userMediaRepo,
                                   IUserRepository userRepo,
                                   IMediaRepository mediaRepo,
                                   //IReviewRepoService reviewRepoService,
                                   IReviewRepository reviewRepo, // VIETOJE _reviewRepoService
                                   IUserMediaAdapter userMediaAdapter,
                                   ILogger<UserMediaController> logger)
        {
            _userMediaRepo = userMediaRepo;
            _userRepo = userRepo;
            _mediaRepo = mediaRepo;
            //_reviewRepoService = reviewRepoService;
            _reviewRepo=reviewRepo; // VIETOJE _reviewRepoService
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

                if (await _userMediaRepo.ExistAsync(m => m.UserId == req.UserId && m.MediaId == req.MediaId))
                {
                    _logger.LogInformation($"{DateTime.Now} AddUserMedia. Media ({req.MediaId}) is already in user's ({req.UserId}) list.");
                    return NotFound($"Media is already in user's list.");
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
        /// Update User Media and it's review by UserMediaId
        /// </summary>
        /// <param name="req">UserMediaId and updatable fields</param>
        /// <returns></returns>
        /// <response code="204">Updated</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Client could not authenticate a request</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("/UpdateUserMediaAndReview")]
        [Authorize(Roles = "admin,user")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateUserMediaAndReview([FromBody] UpdateUserMediaRequest req)
        {
            _logger.LogInformation($"UpdateUserMediaAndReview atempt for UserMediaId - {req.UserMediaId}");
            try
            {
                if (req.UserMediaId <= 0)
                {
                    _logger.LogInformation($"{DateTime.Now} Failed UpdateUserMediaAndReview attempt for UserMediaId - {req.UserMediaId}. UpdateUserMedia request data is invalid.");
                    return BadRequest("UpdateUserMediaAndReview request data is invalid.");
                }

                if (!await _userMediaRepo.ExistAsync(m => m.UserMediaId == req.UserMediaId))
                {
                    _logger.LogInformation($"{DateTime.Now} Failed UpdateUserMediaAndReview attempt with UserMediaId - {req.UserMediaId}. UserMediaId not found.");
                    return NotFound("UserMediaId not found");
                }

                if (!Enum.TryParse<EUserMediaStatus>(req.UserMediaStatus, out _))
                {
                    _logger.LogInformation($"{DateTime.Now} Failed UpdateUserMediaAndReview attempt with UserMediaStatus - {req.UserMediaStatus}. Selected UserMediaStatus is invalid.");
                    return BadRequest("Selected UserMediaStatus is invalid");
                }

                if (!Enum.TryParse<EUserRating>(req.UserRating, out _))
                {
                    _logger.LogInformation($"{DateTime.Now} Failed UpdateUserMediaAndReview attempt with UserRating - {req.UserRating}. Selected UserRating is invalid.");
                    return BadRequest("Selected UserRating is invalid");
                }

                // NEVEIKIA !!! META KLAIDA -> (InvalidOperationException: Error while validating the service descriptor) !!!
                //var userMedia = await _userMediaRepo.GetAsync(a => a.UserMediaId == req.UserMediaId, new List<string> { "Review" });
                //userMedia = await _reviewRepoService.AddReviewIfNeeded(userMedia, req);
                //userMedia = _userMediaAdapter.Bind(userMedia, req);
                //await _userMediaRepo.UpdateAsync(userMedia);

                var userMedia = await _userMediaRepo.GetAsync(a => a.UserMediaId == req.UserMediaId, new List<string> { "User", "Media", "Review" });

                userMedia = await _reviewRepo.AddReviewIfNeeded(userMedia, req);
                userMedia = _userMediaAdapter.Bind(userMedia, req);
                await _userMediaRepo.UpdateAsync(userMedia);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now} UpdateUserMediaAndReview exception error.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Delete address by user id
        /// </summary>
        /// <param name="id">User id whos address will be deleted</param>
        /// <returns></returns>
        /// <response code="204">Updated</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Client could not authenticate a request</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id:int}/Delete")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteUserMediaAndReview(int id)
        {
            _logger.LogInformation($"DeleteUserMediaAndReview atempt for UserMediaId - {id}");
            try
            {
                if (id <= 0)
                {
                    _logger.LogInformation($"{DateTime.Now} Failed DeleteUserMediaAndReview attempt for UserMediaId - {id}. UserMediaId is incorrect.");
                    return BadRequest("UserMediaId is incorrect.");
                }

                var userMedia = await _userMediaRepo.GetAsync(m => m.UserMediaId == id);
                if (userMedia == null)
                {
                    _logger.LogInformation($"{DateTime.Now} Failed DeleteUserMediaAndReview attempt with UserMediaId - {id}. UserMediaId not found.");
                    return NotFound("UserMediaId not found");
                }

                await _userMediaRepo.RemoveAsync(userMedia);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now} DeleteUserMediaAndReview exception error.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


    }
}
