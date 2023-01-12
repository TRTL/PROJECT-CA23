using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PROJECT_CA23.Models
{
    public class Address
    {
        [Required]
        public int AddressId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public virtual User User { get; set; }

        [Required]
        [MaxLength(100)]
        public string Country { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        [Required]
        [MaxLength(500)]
        public string AddressText { get; set; }

        [Required]
        [MaxLength(20)]
        public string PostCode { get; set; }

    }
}
