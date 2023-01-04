using Microsoft.EntityFrameworkCore;
using PROJECT_CA23.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace PROJECT_CA23.Database
{
    public class CA23Context : DbContext
    {
        public CA23Context(DbContextOptions<CA23Context> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var users = modelBuilder.Entity<User>();

            users.HasKey(u => u.UserId);

            users.Property(u => u.Username)
                 .HasMaxLength(100);

            users.Property(u => u.FirstName)
                 .HasMaxLength(200);

            users.Property(u => u.LastName)
                 .HasMaxLength(200);

            users.Property(u => u.Role)
                 .HasConversion<string>()
                 .HasMaxLength(50);
        }
    }
}
