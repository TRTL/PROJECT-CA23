using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PROJECT_CA23.Models;
using PROJECT_CA23.Models.Dto.LoginDto;
using PROJECT_CA23.Models.Enums;
using PROJECT_CA23.Repositories.IRepositories;
using PROJECT_CA23.Services;
using PROJECT_CA23.Services.IServices;

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


        public UserController(IUserRepository userRepository,
                              IUserService userService,
                              IJwtService jwtService,
                              ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _userService = userService;
            _jwtService = jwtService;
            _logger = logger;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest model)
        {
            _logger.LogInformation("Login atempt with username - {username}", model.Username);

            var isOk = _userRepository.TryLogin(model.Username, model.Password, out var user);
            if (!isOk) return Unauthorized("Bad username or password");

            var token = _jwtService.GetJwtToken(user.UserId, user.Role.ToString());

            return Ok(new LoginResponse { Username = model.Username, Token = token });
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("register")]
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
                UserName = model.Username,
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
    }
}
