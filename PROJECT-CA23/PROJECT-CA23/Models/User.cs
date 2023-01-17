using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PROJECT_CA23.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace PROJECT_CA23.Models
{
    public class User
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [MaxLength(200)]
        public string FirstName { get; set; }

        [MaxLength(200)]
        public string LastName { get; set; }

        public ERole Role { get; set; } = ERole.user;

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public DateTime Updated { get; set; }

        public virtual Address? Address { get; set; }
        public virtual List<UserMedia>? UserMedias { get; set; }
    }
}
