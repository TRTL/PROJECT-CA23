﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PROJECT_CA23.Database;

#nullable disable

namespace PROJECTCA23.Migrations
{
    [DbContext(typeof(CA23Context))]
    partial class CA23ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.1");

            modelBuilder.Entity("GenreMedia", b =>
                {
                    b.Property<int>("GenresGenreId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MediasMediaId")
                        .HasColumnType("INTEGER");

                    b.HasKey("GenresGenreId", "MediasMediaId");

                    b.HasIndex("MediasMediaId");

                    b.ToTable("MediaGenre", (string)null);

                    b.HasData(
                        new
                        {
                            GenresGenreId = 1,
                            MediasMediaId = 1
                        },
                        new
                        {
                            GenresGenreId = 2,
                            MediasMediaId = 1
                        },
                        new
                        {
                            GenresGenreId = 3,
                            MediasMediaId = 1
                        },
                        new
                        {
                            GenresGenreId = 4,
                            MediasMediaId = 2
                        },
                        new
                        {
                            GenresGenreId = 2,
                            MediasMediaId = 2
                        },
                        new
                        {
                            GenresGenreId = 5,
                            MediasMediaId = 2
                        });
                });

            modelBuilder.Entity("PROJECT_CA23.Models.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("AddressText")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("PostCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AddressId");

                    b.ToTable("Addresses");

                    b.HasData(
                        new
                        {
                            AddressId = 1,
                            AddressText = "Address X1",
                            City = "City X1",
                            Country = "Country X1",
                            PostCode = "PostCode X1",
                            UserId = 1
                        });
                });

            modelBuilder.Entity("PROJECT_CA23.Models.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("GenreId");

                    b.HasAlternateKey("Name");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            GenreId = 1,
                            Name = "Action"
                        },
                        new
                        {
                            GenreId = 2,
                            Name = "Drama"
                        },
                        new
                        {
                            GenreId = 3,
                            Name = "Sci-Fi"
                        },
                        new
                        {
                            GenreId = 4,
                            Name = "Crime"
                        },
                        new
                        {
                            GenreId = 5,
                            Name = "Thriller"
                        });
                });

            modelBuilder.Entity("PROJECT_CA23.Models.Media", b =>
                {
                    b.Property<int>("MediaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Actors")
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .HasColumnType("TEXT");

                    b.Property<string>("Director")
                        .HasColumnType("TEXT");

                    b.Property<string>("Language")
                        .HasColumnType("TEXT");

                    b.Property<string>("Plot")
                        .HasMaxLength(2000)
                        .HasColumnType("TEXT");

                    b.Property<string>("Poster")
                        .HasColumnType("TEXT");

                    b.Property<string>("Runtime")
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.Property<string>("Writer")
                        .HasColumnType("TEXT");

                    b.Property<string>("Year")
                        .HasMaxLength(9)
                        .HasColumnType("TEXT");

                    b.Property<string>("imdbId")
                        .HasColumnType("TEXT");

                    b.Property<double?>("imdbRating")
                        .HasColumnType("REAL");

                    b.Property<decimal?>("imdbVotes")
                        .HasColumnType("TEXT");

                    b.HasKey("MediaId");

                    b.ToTable("Medias");

                    b.HasData(
                        new
                        {
                            MediaId = 1,
                            Actors = "Harrison Ford, Rutger Hauer, Sean Young",
                            Country = "United States",
                            Director = "Ridley Scott",
                            Language = "English",
                            Plot = "A blade runner must pursue and terminate four replicants who stole a ship in space and have returned to Earth to find their creator.",
                            Poster = "https://m.media-amazon.com/images/M/MV5BNzQzMzJhZTEtOWM4NS00MTdhLTg0YjgtMjM4MDRkZjUwZDBlXkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_SX300.jpg",
                            Runtime = "117 min",
                            Title = "Blade Runner",
                            Type = "movie",
                            Writer = "Hampton Fancher, David Webb Peoples, Philip K. Dick",
                            Year = "2013",
                            imdbId = "tt0083658",
                            imdbRating = 8.0999999999999996,
                            imdbVotes = 771646m
                        },
                        new
                        {
                            MediaId = 2,
                            Actors = "Bryan Cranston, Aaron Paul, Anna Gunn",
                            Country = "United States",
                            Language = "English",
                            Plot = "A chemistry teacher diagnosed with inoperable lung cancer turns to manufacturing and selling methamphetamine with a former student in order to secure his family's future.",
                            Poster = "https://m.media-amazon.com/images/M/MV5BYTU3NWI5OGMtZmZhNy00MjVmLTk1YzAtZjA3ZDA3NzcyNDUxXkEyXkFqcGdeQXVyODY5Njk4Njc@._V1_SX300.jpg",
                            Runtime = "49 min",
                            Title = "Breaking Bad",
                            Type = "series",
                            Writer = "Vince Gilligan",
                            Year = "2008–2013",
                            imdbId = "tt0903747",
                            imdbRating = 9.5,
                            imdbVotes = 1880303m
                        });
                });

            modelBuilder.Entity("PROJECT_CA23.Models.Notification", b =>
                {
                    b.Property<int>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Shown")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Text")
                        .HasMaxLength(1000)
                        .HasColumnType("INTEGER");

                    b.Property<int>("Title")
                        .HasMaxLength(100)
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("NotificationId");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("PROJECT_CA23.Models.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MediaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ReviewText")
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserMediaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserRating")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("ReviewId");

                    b.HasIndex("MediaId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("PROJECT_CA23.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.HasAlternateKey("Username");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Created = new DateTime(2023, 1, 16, 18, 31, 31, 14, DateTimeKind.Local).AddTicks(1094),
                            FirstName = "Jonas",
                            LastName = "Jonaitis",
                            PasswordHash = new byte[] { 142, 86, 160, 57, 248, 0, 147, 16, 27, 190, 251, 38, 107, 80, 200, 64, 148, 58, 154, 79, 87, 241, 70, 88, 223, 73, 35, 138, 8, 169, 135, 251 },
                            PasswordSalt = new byte[] { 199, 138, 18, 216, 156, 153, 161, 18, 1, 178, 38, 90, 74, 28, 92, 126, 7, 158, 117, 193, 39, 55, 222, 241, 209, 170, 103, 223, 87, 32, 164, 222, 37, 23, 243, 209, 241, 235, 53, 93, 238, 202, 79, 246, 131, 63, 15, 223, 88, 183, 122, 57, 59, 122, 219, 173, 25, 122, 216, 173, 191, 195, 54, 58 },
                            Role = "admin",
                            Updated = new DateTime(2023, 1, 16, 18, 31, 31, 18, DateTimeKind.Local).AddTicks(3056),
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("PROJECT_CA23.Models.UserMedia", b =>
                {
                    b.Property<int>("UserMediaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("MediaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Note")
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<int?>("ReviewId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserMediaStatus")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("UserMediaId");

                    b.HasAlternateKey("UserId", "MediaId");

                    b.HasIndex("MediaId");

                    b.ToTable("UserMedias");
                });

            modelBuilder.Entity("GenreMedia", b =>
                {
                    b.HasOne("PROJECT_CA23.Models.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresGenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PROJECT_CA23.Models.Media", null)
                        .WithMany()
                        .HasForeignKey("MediasMediaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PROJECT_CA23.Models.Address", b =>
                {
                    b.HasOne("PROJECT_CA23.Models.User", "User")
                        .WithOne("Address")
                        .HasForeignKey("PROJECT_CA23.Models.Address", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PROJECT_CA23.Models.Notification", b =>
                {
                    b.HasOne("PROJECT_CA23.Models.User", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PROJECT_CA23.Models.Review", b =>
                {
                    b.HasOne("PROJECT_CA23.Models.Media", "Media")
                        .WithMany("Reviews")
                        .HasForeignKey("MediaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PROJECT_CA23.Models.UserMedia", "UserMedia")
                        .WithOne("Review")
                        .HasForeignKey("PROJECT_CA23.Models.Review", "ReviewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PROJECT_CA23.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Media");

                    b.Navigation("User");

                    b.Navigation("UserMedia");
                });

            modelBuilder.Entity("PROJECT_CA23.Models.UserMedia", b =>
                {
                    b.HasOne("PROJECT_CA23.Models.Media", "Media")
                        .WithMany()
                        .HasForeignKey("MediaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PROJECT_CA23.Models.User", "User")
                        .WithMany("UserMedias")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Media");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PROJECT_CA23.Models.Media", b =>
                {
                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("PROJECT_CA23.Models.User", b =>
                {
                    b.Navigation("Address");

                    b.Navigation("Notifications");

                    b.Navigation("UserMedias");
                });

            modelBuilder.Entity("PROJECT_CA23.Models.UserMedia", b =>
                {
                    b.Navigation("Review");
                });
#pragma warning restore 612, 618
        }
    }
}
