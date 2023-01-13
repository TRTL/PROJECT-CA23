using System.ComponentModel.DataAnnotations;
using PROJECT_CA23.Models.Dto.UserDtos;

namespace PROJECT_CA23.Models.Dto.AddressDto
{
    public class AddressDto
    {
        /// <summary>
        /// Id of user that this address belongs to
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// First name of user that this address belongs to
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last name of user that this address belongs to
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Country users lives in
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// City users lives in
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Full address users lives at. Like Street, house no. and so on
        /// </summary>
        public string AddressText { get; set; }
        /// <summary>
        /// Post code of address
        /// </summary>
        public string PostCode { get; set; }
    }
}
