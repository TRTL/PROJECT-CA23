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

namespace PROJECT_CA23.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IUserRepository userRepository,
                              ILogger<UserController> logger,
                              IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _logger = logger;
            _httpContextAccessor=httpContextAccessor;
        }

        /// <summary>
        /// Get user information
        /// </summary>
        /// <param name="id">Id of user</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Client could not authenticate a request</response>
        /// <response code="500">Internal server error</response>
        [Authorize(Roles = "admin,user")]
        [HttpGet("/GetUser/{id:int}/Info")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        public IActionResult GetUserInfo(int id)
        {
            try
            {
                var currentUserRole = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
                var currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
                if (currentUserRole != "admin" && currentUserId != id)
                {
                    _logger.LogWarning("User {currentUserId} tried to access user {id} info", currentUserId, id);
                    return Forbid();
                }

                var user = _userRepository.Get(id);

                var userDto = new UserDto
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Role = user.Role.ToString(),
                    Created = user.Created,
                    Updated = user.Updated,
                    LastLogin = user.LastLogin,
                    IsDeleted = user.IsDeleted
                };

                return Ok(userDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now} Login exception error.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
