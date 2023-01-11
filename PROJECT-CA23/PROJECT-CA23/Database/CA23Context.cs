using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PROJECT_CA23.Database.InitialData;
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
            user.HasKey(u => u.UserId);
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
            user.HasData(CA23InitialData.userInitialDataSeed);


            var address = modelBuilder.Entity<Address>();
            address.HasKey(a => a.AddressId);
            address.HasData(CA23InitialData.addressInitialDataSeed);


            var genre = modelBuilder.Entity<Genre>();
            genre.HasKey(genr => genr.GenreId);
            genre.HasAlternateKey(genr => genr.Name);
            genre.HasData(CA23InitialData.genreInitialDataSeed);


            var media = modelBuilder.Entity<Media>();
            media.HasKey(med => med.MediaId);
            media.HasMany(med => med.Reviews)
                 .WithOne(rev => rev.Media)
                 .HasForeignKey(rev => rev.MediaId); // Media 1 - 100 Reviews
            media.HasMany(med => med.Genres)
                 .WithMany(genr => genr.Medias)
                 .UsingEntity(ent => ent.ToTable("MediaGenre")); // Media 100 - 100 Genre
            media.HasData(CA23InitialData.mediaInitialDataSeed);

            modelBuilder.Entity<Media>()
                        .HasMany(med => med.Genres)
                        .WithMany(genr => genr.Medias)
                        .UsingEntity(ent => ent.HasData(CA23InitialData.mediaGenreInitialDataSeed));


            var review = modelBuilder.Entity<Review>();
            review.HasKey(r => new { r.UserId, r.MediaId });
            review.Property(r => r.UserRating)
                  .HasConversion<string>()
                  .HasMaxLength(50);


            var notification = modelBuilder.Entity<Notification>();
            notification.HasKey(n => n.NotificationId);
            notification.HasOne(n => n.User)
                        .WithMany(u => u.Notifications)
                        .HasForeignKey(n => n.UserId); // User 1 - 100 Notifications
        }
    }
}
