﻿using Microsoft.AspNetCore.Authorization;
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
    [Route("[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly IMediaRepository _mediaRepo;
        private readonly ILogger<MediaController> _logger;
        private readonly IMediaAdapter _mediaAdapter;
        private readonly IMediaService _mediaService;
        private readonly IGenreRepository _genreRepo;

        public MediaController(IMediaRepository mediaRepo,
                               ILogger<MediaController> logger,
                               IMediaAdapter mediaAdapter,
                               IMediaService mediaService,
                               IGenreRepository genreRepo)
        {
            _mediaRepo = mediaRepo;
            _logger = logger;
            _mediaAdapter = mediaAdapter;
            _mediaService = mediaService;
            _genreRepo = genreRepo;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
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
        /// Get list of all medias
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Client could not authenticate a request</response>
        /// <response code="500">Internal server error</response>
        [Authorize(Roles = "admin")]
        [HttpGet("/GetAllMedias")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MediaDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAllMedias()
        {
            _logger.LogInformation($"GetAllMedias atempt");
            try
            {
                var allMedia = await _mediaRepo.GetAllAsync(null, new List<string>() { "Genres" });
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
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Client could not authenticate a request</response>
        /// <response code="500">Internal server error</response>
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

                List<Genre>? mediaGenres = await _genreRepo.AddNewAndGetExistingGenresOfThisMedia(req.Genre);
                
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


        /// <summary>
        /// Deleting Media by mediaId
        /// </summary>
        /// <param name="id">Media Id</param>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        [HttpDelete("/DeleteMedia/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMedia(int id)
        {
            _logger.LogInformation($"DeleteMedia atempt with id - {id}");

            try
            {
                var mediaExist = await _mediaRepo.ExistAsync(m => m.MediaId == id);
                if (!mediaExist)
                {
                    _logger.LogInformation("Media with id - {id} not found", id);
                    return NotFound();
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
