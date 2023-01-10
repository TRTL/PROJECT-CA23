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
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "admin,user")]
        [HttpGet("/GetUserAddress")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddressDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUserAddress(int id)
        {
            // ISKELTI I ATSKIRA SERVISIUKA (su ten pakrautu _httpContextAccessor)
            var currentUserRole = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            var currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
            if (currentUserRole != "admin" && currentUserId != id)
            {
                _logger.LogWarning("User {currentUserId} tried to access user {id} info", currentUserId, id);
                return Forbid();
            }

            var address = _addressRepo.GetByUserId(id).Result;
            if (address == null) return NotFound("User does not have an address");

            var addressDto = _addressAdapter.Bind(address);
            return Ok(addressDto);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addressDto"></param>
        /// <returns></returns>
        [Authorize(Roles = "admin,user")]
        [HttpPost("/AddUserAddress")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult AddAddress(AddressRequest req)
        {
            // ISKELTI I ATSKIRA SERVISIUKA (su ten pakrautu _httpContextAccessor)
            var currentUserRole = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            var currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
            if (currentUserRole != "admin" && currentUserId != req.UserId)
            {
                _logger.LogWarning("User {currentUserId} tried to access user {id} info", currentUserId, req.UserId);
                return Forbid();
            }

            var user = _userRepo.Get(req.UserId);

            Address newAddress = _addressAdapter.Bind(req, user);
            var newAddressId = _addressRepo.Create(newAddress);

            user.Address = newAddress;
            //user.AddressId = newAddressId;
            _userRepo.Update(user);

            return CreatedAtAction(nameof(GetUserAddress), newAddressId);
        }




    }
}
