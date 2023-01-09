using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PROJECT_CA23.Models;
using PROJECT_CA23.Models.Dto.LoginDto;
using PROJECT_CA23.Models.Dto.UserDto;
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
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private readonly ILogger<UserController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserController(IUserRepository userRepository,
                              IUserService userService,
                              IJwtService jwtService,
                              ILogger<UserController> logger,
                              IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _userService = userService;
            _jwtService = jwtService;
            _logger = logger;
            _httpContextAccessor=httpContextAccessor;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        public IActionResult Login([FromBody] LoginRequest model)
        {
            _logger.LogInformation("Login atempt with username - {username}", model.Username);

            var isOk = _userRepository.TryLogin(model.Username, model.Password, out var user);
            if (!isOk) return Unauthorized("Bad username or password");

            var token = _jwtService.GetJwtToken(user.UserId, user.Role.ToString());

            return Ok(new LoginResponse
            {
                Username = model.Username,
                Token = token 
            });
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        public IActionResult Register([FromBody] RegisterUserRequest model)
        {
            if (_userRepository.Exist(model.Username)) return BadRequest("User already exists");

            ERole role = ERole.user;
            if (!Enum.TryParse<ERole>(model.Role, out role)) return BadRequest("Selected Role is invalid");

            _userService.CreatePasswordHash(model.Password, out var passwordHash, out var passwordSalt);

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username,
                Role = role,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
                LastLogin = DateTime.UtcNow,
                IsDeleted = false
            };

            var id = _userRepository.Register(user);

            return Created(nameof(Login), new { id = id });
        }

        [Authorize(Roles = "admin,user")]
        [HttpGet("GetUserInfo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        public IActionResult GetUserInfo(int id)
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
    }
}
