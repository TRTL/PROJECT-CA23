using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PROJECT_CA23.Models;
using PROJECT_CA23.Models.Dto.LoginDtos;
using PROJECT_CA23.Models.Dto.UserDtos;
using PROJECT_CA23.Models.Enums;
using PROJECT_CA23.Repositories.IRepositories;
using PROJECT_CA23.Services;
using PROJECT_CA23.Services.IServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Data;
using System.Security.Claims;
using PROJECT_CA23.Repositories;
using PROJECT_CA23.Services.Adapters.IAdapters;
using System.Net.Mime;

namespace PROJECT_CA23.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IUserAdapter _userAdapter;
        private readonly ILogger<UserController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IUserRepository userRepository,
                              IUserAdapter userAdapter,
                              ILogger<UserController> logger,
                              IHttpContextAccessor httpContextAccessor)
        {
            _userRepo = userRepository;
            _userAdapter = userAdapter;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Get user information
        /// </summary>
        /// <param name="id">Id of user</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Client could not authenticate a request</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal server error</response>
        [Authorize(Roles = "admin,user")]
        [HttpGet("/GetUser/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult GetUserInfo(int id)
        {
            _logger.LogInformation($"GetUserInfo atempt with UserId - {id}");
            try
            {
                var currentUserRole = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
                var currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
                if (currentUserRole != "admin" && currentUserId != id)
                {
                    _logger.LogWarning("User {currentUserId} tried to access user {id} info", currentUserId, id);
                    return Forbid();
                }

                if (!_userRepo.Exist(id, out User? user))
                {
                    _logger.LogInformation($"{DateTime.Now} Failed GetUserInfo attempt with userId - {id}. UserId not found.");
                    return NotFound("UserId not found");
                }

                UserDto userDto = _userAdapter.Bind(user);
                return Ok(userDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now} GetUserInfo exception error.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get information all users
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Client could not authenticate a request</response>
        /// <response code="500">Internal server error</response>
        [Authorize(Roles = "admin")]
        [HttpGet("/GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserDto>))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult GetAllUsers()
        {
            _logger.LogInformation($"GetAllUsers atempt");
            try
            {
                var users = _userRepo.GetAll();
                var listOfUserDto = users.Select(u => _userAdapter.Bind(u)).ToList();
                return Ok(listOfUserDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now} GetAllUsers exception error.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Update user information
        /// </summary>
        /// <param name="id">User id that will be updated</param>
        /// <param name="updateUserDto">Updatable fields: first and last name</param>
        /// <returns></returns>
        /// <response code="204">Updated</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Client could not authenticate a request</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id:int}/Update/")]
        [Authorize(Roles = "admin,user")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
        {
            _logger.LogInformation($"UpdateUser atempt for userId - {id}");

            try
            {
                if (id == 0 || updateUserDto == null)
                {
                    _logger.LogInformation($"{DateTime.Now} Failed UpdateUser attempt with userId - {id}. UpdateUser request data incorrect.");
                    return BadRequest("UpdateUser request data incorrect.");
                }

                if (!_userRepo.Exist(id, out User? user))
                {
                    _logger.LogInformation($"{DateTime.Now} Failed UpdateUser attempt with userId - {id}. UserId not found.");
                    return NotFound("UserId not found");
                }

                var updatedUser = _userAdapter.Bind(user, updateUserDto);
                _userRepo.Update(updatedUser);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now} UpdateUser exception error.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }









    }
}
