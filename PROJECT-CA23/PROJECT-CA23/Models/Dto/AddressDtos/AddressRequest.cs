using PROJECT_CA23.Models.Dto.UserDtos;

namespace PROJECT_CA23.Models.Dto.AddressDtos
{
    public class AddressRequest
    {
        public int UserId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string AddressText { get; set; }
        public string PostCode { get; set; }
    }
}
