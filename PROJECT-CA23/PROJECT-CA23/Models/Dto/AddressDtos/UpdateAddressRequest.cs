using PROJECT_CA23.Models.Dto.UserDtos;
using System.ComponentModel.DataAnnotations;

namespace PROJECT_CA23.Models.Dto.AddressDtos
{
    public class UpdateAddressRequest
    {
        /// <summary>
        /// Id of address that will be updated
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// Country users lives in
        /// </summary>
        [MaxLength(100, ErrorMessage = "Country cannot be longer than 100 characters")]
        public string Country { get; set; }

        /// <summary>
        /// City users lives in
        /// </summary>
        [MaxLength(100, ErrorMessage = "City cannot be longer than 100 characters")]
        public string City { get; set; }

        /// <summary>
        /// Full address users lives at. Like Street, house no. and so on
        /// </summary>
        [MaxLength(500, ErrorMessage = "AddressText cannot be longer than 500 characters")]
        public string AddressText { get; set; }

        /// <summary>
        /// Post code of address
        /// </summary>
        /// 
        [MaxLength(20, ErrorMessage = "PostCode cannot be longer than 20 characters")]
        public string PostCode { get; set; }
    }
}
