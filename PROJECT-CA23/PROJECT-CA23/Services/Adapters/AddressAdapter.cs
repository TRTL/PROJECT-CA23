using PROJECT_CA23.Models;
using PROJECT_CA23.Models.Dto.AddressDto;
using PROJECT_CA23.Models.Dto.AddressDtos;
using PROJECT_CA23.Models.Dto.UserDtos;
using PROJECT_CA23.Services.Adapters.IAdapters;

namespace PROJECT_CA23.Services.Adapters
{
    public class AddressAdapter : IAddressAdapter
    {
        public AddressDto Bind(Address address)
        {
            var dto = new AddressDto()
            {
                UserId = address.UserId,
                FirstName = address.User.FirstName,
                LastName = address.User.FirstName,
                Country = address.Country,
                City = address.City,
                AddressText = address.AddressText,
                PostCode = address.PostCode                
            };
            return dto;
        }

        public Address Bind(AddAddressRequest req, User user)
        {
            var newAddress = new Address()
            {
                UserId = req.UserId,
                User = user,
                City = req.City,
                Country = req.Country,
                AddressText = req.AddressText,
                PostCode = req.PostCode
            };
            return newAddress;
        }

        public IEnumerable<AddressDto> Bind(IEnumerable<Address> addresses)
        {
            var listOfAddressDto = new List<AddressDto>();
            foreach (var address in addresses)
            {
                listOfAddressDto.Add(Bind(address));
            }
            return listOfAddressDto;
        }
    }
}
