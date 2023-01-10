using PROJECT_CA23.Models;
using PROJECT_CA23.Models.Dto.AddressDto;

namespace PROJECT_CA23.Services.Adapters.IAdapters
{
    public interface IAddressAdapter
    {
        Address Bind(AddressDto dto);
        AddressDto Bind(Address address);
    }
}
