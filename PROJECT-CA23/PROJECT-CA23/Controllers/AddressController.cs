using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PROJECT_CA23.Models;
using PROJECT_CA23.Models.Dto.UserDto;
using PROJECT_CA23.Repositories;
using PROJECT_CA23.Repositories.IRepositories;
using System.Security.Claims;

namespace PROJECT_CA23.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<UserController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddressController(IAddressRepository addressRepository, 
                                 ILogger<UserController> logger, 
                                 IHttpContextAccessor httpContextAccessor)
        {
            _addressRepository = addressRepository;
            _logger=logger;
            _httpContextAccessor=httpContextAccessor;
        }

        [Authorize(Roles = "admin,user")]
        [HttpGet("GetUserAddress")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Address))]
        public IActionResult GetUserAddress(int id)
        {
            var currentUserRole = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            var currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
            if (currentUserRole != "admin" && currentUserId != id)
            {
                _logger.LogWarning("User {currentUserId} tried to access user {id} info", currentUserId, id);
                return Forbid();
            }

            var address = _addressRepository.GetByUserId(id);

            return Ok(address);
        }




    }
}
