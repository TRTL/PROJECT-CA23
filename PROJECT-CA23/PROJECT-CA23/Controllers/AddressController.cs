using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PROJECT_CA23.Models;
using PROJECT_CA23.Models.Dto.AddressDto;
using PROJECT_CA23.Models.Dto.AddressDtos;
using PROJECT_CA23.Models.Dto.UserDtos;
using PROJECT_CA23.Repositories;
using PROJECT_CA23.Repositories.IRepositories;
using PROJECT_CA23.Services.Adapters.IAdapters;
using System.Net;
using System.Security.Claims;

namespace PROJECT_CA23.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository _addressRepo;
        private readonly IUserRepository _userRepo;
        private readonly IAddressAdapter _addressAdapter;
        private readonly ILogger<UserController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddressController(IAddressRepository addressRepository,
                                 IUserRepository userRepo,
                                 IAddressAdapter addressAdapter,
                                 ILogger<UserController> logger,
                                 IHttpContextAccessor httpContextAccessor)
        {
            _addressRepo = addressRepository;
            _userRepo = userRepo;
            _addressAdapter = addressAdapter;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }


        /// <summary>
        /// Get address of a user by userId
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Client could not authenticate a request</response>
        /// <response code="500">Internal server error</response>
        [Authorize(Roles = "admin,user")]
        [HttpGet("/GetAddressbyId/{id:int}", Name = "GetAddress")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddressDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAddress(int id)
        {
            _logger.LogInformation($"GetAddress atempt for userId - {id}");

            try
            {
                // ISKELTI I ATSKIRA SERVISIUKA (su ten pakrautu _httpContextAccessor) ???
                var currentUserRole = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
                var currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
                if (currentUserRole != "admin" && currentUserId != id)
                {
                    _logger.LogWarning("User {currentUserId} tried to access user {id} info", currentUserId, id);
                    return Forbid();
                }

                var address = _addressRepo.GetAsync(a => a.UserId == id, new List<string>() { "User" }).Result;
                if (address == null) return NotFound("User does not have an address");

                var addressDto = _addressAdapter.Bind(address);
                return Ok(addressDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now} GetAddress exception error.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Get list of all addresses
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Client could not authenticate a request</response>
        /// <response code="500">Internal server error</response>
        [Authorize(Roles = "admin")]
        [HttpGet("/GetAllAddresses")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AddressDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAddresses()
        {
            _logger.LogInformation($"GetAllAddresses atempt");

            try
            {
                // ISKELTI I ATSKIRA SERVISIUKA (su ten pakrautu _httpContextAccessor) ???
                var currentUserRole = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
                var currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
                if (currentUserRole != "admin")
                {
                    _logger.LogWarning("User Id:{currentUserId}, with Role:{role} tried to access info that requares Admin role", currentUserId, currentUserRole);
                    return Forbid();
                }

                var allAddresses = await _addressRepo.GetAllAsync();
                var addressDtoList = allAddresses.Select(a => _addressAdapter.Bind(a))
                                                 .ToList();

                return Ok(addressDtoList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now} GetAllAddresses exception error.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Add address information to user by userId 
        /// </summary>
        /// <param name="req">UserId, Country, City, AddressText and PostCode</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Client could not authenticate a request</response>
        /// <response code="500">Internal server error</response>
        [Authorize(Roles = "admin,user")]
        [HttpPost("/AddAddress")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AddressDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAddress([FromBody] AddressRequest req)
        {
            _logger.LogInformation($"AddAddress atempt for userId - {req.UserId}");

            try
            {
                // ISKELTI I ATSKIRA SERVISIUKA (su ten pakrautu _httpContextAccessor) ???
                var currentUserRole = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
                var currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
                if (currentUserRole != "admin" && currentUserId != req.UserId)
                {
                    _logger.LogWarning("User {currentUserId} tried to access user {id} info", currentUserId, req.UserId);
                    return Forbid();
                }

                var user = _userRepo.Get(req.UserId);
                var newAddress = _addressAdapter.Bind(req, user);

                await _addressRepo.CreateAsync(newAddress);

                return CreatedAtRoute("GetAddress", new { id = newAddress.AddressId }, _addressAdapter.Bind(newAddress));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now} AddAddress exception error.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }







    }
}
