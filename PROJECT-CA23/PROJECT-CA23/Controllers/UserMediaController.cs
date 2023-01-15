using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PROJECT_CA23.Models.Dto.MediaDtos;
using PROJECT_CA23.Models.Dto.UserMediaDtos;
using PROJECT_CA23.Repositories.IRepositories;
using PROJECT_CA23.Services.Adapters.IAdapters;
using System.Net.Mime;

namespace PROJECT_CA23.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserMediaController : ControllerBase
    {
        private readonly IUserMediaRepository _userMediaRepo;
        private readonly IUserMediaAdapter _userMediaAdapter;
        private readonly ILogger<UserMediaController> _logger;

        public UserMediaController(IUserMediaRepository userMediaRepo,
                                   IUserMediaAdapter userMediaAdapter,
                                   ILogger<UserMediaController> logger)
        {
            _userMediaRepo = userMediaRepo;
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
        [Authorize(Roles = "admin")]
        [HttpGet("/GetAllUserMedias")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserMediaDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAllUserMedias([FromBody] UserMediaRequest req)
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
    }
}
