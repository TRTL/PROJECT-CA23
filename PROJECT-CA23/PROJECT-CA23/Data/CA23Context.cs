using Microsoft.EntityFrameworkCore;
using PROJECT_CA23.Models;

namespace PROJECT_CA23.Data
{
    public class CA23Context : DbContext
    {
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var users = modelBuilder.Entity<User>();
            users.HasKey(u => u.UserId);
        }
    }
}
