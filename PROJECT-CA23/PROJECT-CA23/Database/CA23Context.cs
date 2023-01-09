using Microsoft.EntityFrameworkCore;
using PROJECT_CA23.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace PROJECT_CA23.Database
{
    public class CA23Context : DbContext
    {
        public CA23Context(DbContextOptions<CA23Context> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var user = modelBuilder.Entity<User>();

            user.Property(u => u.Role)
                .HasConversion<string>()
                .HasMaxLength(50);

            user.HasOne(u => u.Address)
                .WithOne(a => a.User)
                .HasForeignKey<Address>(a => a.AddressId); // User 1 - 1 Address

            //var address = modelBuilder.Entity<Address>();


        }
    }
}
