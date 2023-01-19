using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PROJECT_CA23.Models.Dto.AddressDto;
using PROJECT_CA23.Models.Dto.MediaDtos;
using PROJECT_CA23.Repositories.IRepositories;
using PROJECT_CA23.Services.Adapters.IAdapters;

namespace PROJECT_CA23.Controllers
{
    /// <summary>
    /// Get genres of media that are saved on our db
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository _genreRepo;
        private readonly ILogger<GenreController> _logger;
        //private readonly IGenreAdapter _genreAdapter;

        public GenreController(IGenreRepository genreRepo, ILogger<GenreController> logger)
        {
            _genreRepo = genreRepo;
            _logger = logger;
        }

        /// <summary>
        /// Get all media with specific genre id
        /// </summary>
        /// <param name="id">Genre Id</param>
        /// <returns></returns>
        /// <response code="200">Indicates that the request has succeeded</response>
        /// <response code="400">Server cannot or will not process the request</response>
        /// <response code="401">Client request has not been completed because it lacks valid authentication credentials for the requested resource</response>
        /// <response code="500">Server encountered an unexpected condition that prevented it from fulfilling the request</response>
        [Authorize(Roles = "admin,user")]
        [HttpGet("/GetAllMediaByGenreId/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MediaDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllMediaByGenreId(int id)
        {
            _logger.LogInformation($"GetAllMediasByGenreId atempt with GenreId - {id}");

            try
            {
                if (id <= 0)
                {
                    _logger.LogInformation($"{DateTime.Now} Failed GetAllMediaByGenreId attempt for GenreId - {id}. GenreId is incorrect.");
                    return BadRequest("GenreId is incorrect.");
                }

                var genre = _genreRepo.GetAsync(g => g.GenreId == id, new List<string>() { "Medias" }).Result;
                return Ok(genre);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now} GetAllMediasByGenreId exception error.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
