using PROJECT_CA23.Models;
using PROJECT_CA23.Models.Dto.AddressDto;
using PROJECT_CA23.Models.Dto.AddressDtos;

namespace PROJECT_CA23.Services.Adapters.IAdapters
{
    public interface IAddressAdapter
    {
        //Address Bind(AddressDto dto, User user);
        AddressDto Bind(Address address);
        Address Bind(AddressRequest req, User user);
    }
}
