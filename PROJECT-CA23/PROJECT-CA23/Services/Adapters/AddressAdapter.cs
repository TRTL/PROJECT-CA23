using PROJECT_CA23.Models;
using PROJECT_CA23.Models.Dto.AddressDto;
using PROJECT_CA23.Models.Dto.UserDtos;
using PROJECT_CA23.Services.Adapters.IAdapters;

namespace PROJECT_CA23.Services.Adapters
{
    public class AddressAdapter : IAddressAdapter
    {
        public Address Bind(AddressDto dto)
        {
            var newAddress = new Address()
            {
                UserId = dto.UserId,
                City = dto.City,
                Country = dto.Country,
                AddressText = dto.AddressText,
                PostCode = dto.PostCode
            };
            return newAddress;
        }

        public AddressDto Bind(Address address)
        {
            var dto = new AddressDto()
            {
                Country = address.Country,
                City = address.City,
                AddressText = address.AddressText,
                PostCode = address.PostCode,
                UserId = address.UserId,
                User = new UserDto()
                {  
                    UserId = address.User.UserId,
                    FirstName = address.User.FirstName,
                    LastName = address.User.FirstName,
                    Username = address.User.Username,
                    Role = address.User.Role.ToString(),
                    Created = address.User.Created,
                    Updated = address.User.Updated,
                    LastLogin = address.User.LastLogin,
                    IsDeleted = address.User.IsDeleted,
                }
            };
            return dto;
        }
    }
}
