using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PROJECT_CA23.Models.Dto.LoginDtos;
using PROJECT_CA23.Models.Enums;
using PROJECT_CA23.Models;
using PROJECT_CA23.Repositories.IRepositories;
using PROJECT_CA23.Services.IServices;

namespace PROJECT_CA23.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private readonly ILogger<UserController> _logger;

        public LoginController(IUserRepository userRepository,
                               IUserService userService,
                               IJwtService jwtService,
                               ILogger<UserController> logger,
                               IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _userService = userService;
            _jwtService = jwtService;
            _logger = logger;
        }

        /// <summary>
        /// Tries to login user with username and password
        /// </summary>
        /// <param name="model">username and password</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Client could not authenticate a request</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("/Login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public IActionResult Login([FromBody] LoginRequest model)
        {
            _logger.LogInformation($"Login atempt with username - {model.Username}");

            try
            {
                var isOk = _userRepository.TryLogin(model.Username, model.Password, out var user);
                if (!isOk)
                {
                    _logger.LogInformation($"{DateTime.Now} Failed Login attempt with username - {model.Username} and password - {model.Password}.");
                    return BadRequest(new { message = "Username or password is incorrect" });
                }

                var token = _jwtService.GetJwtToken(user.UserId, user.Role.ToString());

                return Ok(new LoginResponse
                {
                    UserId = user.UserId,
                    Token = token
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now} Login exception error.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="model">First name, last name, role, username and password</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Client could not authenticate a request</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("/Register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public IActionResult Register([FromBody] RegisterUserRequest model)
        {
            try
            {
                if (_userRepository.Exist(model.Username))
                {
                    _logger.LogInformation($"{DateTime.Now} Failed registration attempt with username - {model.Username}. Username already exists.");
                     return BadRequest("Username already exists");
                }

                ERole role = ERole.user;
                if (!Enum.TryParse<ERole>(model.Role, out role)) 
                {
                    _logger.LogInformation($"{DateTime.Now} Failed registration attempt with role - {model.Role}. Selected Role is invalid.");
                    return BadRequest("Selected Role is invalid");
                }

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
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now} Registration exception error.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
