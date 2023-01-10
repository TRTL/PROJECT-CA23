using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PROJECT_CA23.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace PROJECT_CA23.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [MaxLength(100, ErrorMessage = "Username cannot be longer than 100 symbols")]
        public string Username { get; set; }

        [MaxLength(200, ErrorMessage = "First name cannot be longer than 200 symbols")]
        public string FirstName { get; set; }

        [MaxLength(200, ErrorMessage = "Last name cannot be longer than 200 symbols")]
        public string LastName { get; set; }

        public ERole Role { get; set; } = ERole.user;

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public DateTime LastLogin { get; set; }

        public bool IsDeleted { get; set; } = false;

        public virtual Address? Address { get; set; }
    }
}
