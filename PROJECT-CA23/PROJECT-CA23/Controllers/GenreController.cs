using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PROJECT_CA23.Models.Dto.AddressDto;
using PROJECT_CA23.Repositories.IRepositories;
using PROJECT_CA23.Services.Adapters.IAdapters;

namespace PROJECT_CA23.Controllers
{
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
        /// Get all media with specific genre
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Client could not authenticate a request</response>
        /// <response code="500">Internal server error</response>
        [Authorize(Roles = "admin,user")]
        [HttpGet("/GetAllMediaByGenreId/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)] // , Type = typeof(AddressDto)
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllMediaByGenreId(int id)
        {
            _logger.LogInformation($"GetAllMediasByGenreId atempt with GenreId - {id}");

            try
            {
                if (id == 0) { }

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
