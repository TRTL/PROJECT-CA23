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
        public DbSet<Media> Medias { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<UserMedia> UserMedias { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var user = modelBuilder.Entity<User>();

            user.HasAlternateKey(u => u.Username);

            user.Property(user => user.Role)
                .HasConversion<string>()
                .HasMaxLength(50);

            user.HasOne(user => user.Address)
                .WithOne(adr => adr.User)
                .HasForeignKey<Address>(adr => adr.AddressId); // User 1 - 1 Address

            user.HasMany(user => user.UserMedias)
                .WithOne(medi => medi.User)
                .HasForeignKey(media => media.UserId); // User 1 - 100 UserMedias

            user.HasMany(user => user.Notifications)
                .WithOne(noti => noti.User)
                .HasForeignKey(noti => noti.UserId); // User 1 - 100 Notifications


            var media = modelBuilder.Entity<Media>();

            media.HasMany(m => m.Reviews)
                 .WithOne(media => media.Media)
                 .HasForeignKey(media => media.MediaId); // Media 1 - 100 Reviews

            media.HasMany(m => m.Genres)
                 .WithMany(g => g.Medias)
                 .UsingEntity(e => e.ToTable("MediaGenre")); // Media 100 - 100 Genre


            var review = modelBuilder.Entity<Review>();

            review.HasKey(r => new { r.UserId, r.MediaId }); // Composite key

            review.Property(r => r.UserRating)
                  .HasConversion<string>()
                  .HasMaxLength(50);


            var notification = modelBuilder.Entity<Notification>();
            notification.HasOne(n => n.User)
                        .WithMany(u => u.Notifications)
                        .HasForeignKey(n => n.UserId); // User 1 - 100 Notifications


            //var genre = modelBuilder.Entity<Genre>();
            //genre.HasAlternateKey(g => g.Name);

        }
    }
}
