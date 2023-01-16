using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROJECTCA23.Migrations
{
    /// <inheritdoc />
    public partial class userIsDeletedAndLastLoginRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastLogin",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Medias",
                keyColumn: "MediaId",
                keyValue: 1,
                columns: new[] { "Actors", "Country", "Plot" },
                values: new object[] { "Harrison Ford, Rutger Hauer, Sean Young", "United States", "A blade runner must pursue and terminate four replicants who stole a ship in space and have returned to Earth to find their creator." });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "Created", "PasswordHash", "PasswordSalt", "Updated" },
                values: new object[] { new DateTime(2023, 1, 16, 18, 31, 31, 14, DateTimeKind.Local).AddTicks(1094), new byte[] { 142, 86, 160, 57, 248, 0, 147, 16, 27, 190, 251, 38, 107, 80, 200, 64, 148, 58, 154, 79, 87, 241, 70, 88, 223, 73, 35, 138, 8, 169, 135, 251 }, new byte[] { 199, 138, 18, 216, 156, 153, 161, 18, 1, 178, 38, 90, 74, 28, 92, 126, 7, 158, 117, 193, 39, 55, 222, 241, 209, 170, 103, 223, 87, 32, 164, 222, 37, 23, 243, 209, 241, 235, 53, 93, 238, 202, 79, 246, 131, 63, 15, 223, 88, 183, 122, 57, 59, 122, 219, 173, 25, 122, 216, 173, 191, 195, 54, 58 }, new DateTime(2023, 1, 16, 18, 31, 31, 18, DateTimeKind.Local).AddTicks(3056) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogin",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Medias",
                keyColumn: "MediaId",
                keyValue: 1,
                columns: new[] { "Actors", "Country", "Plot" },
                values: new object[] { "Stephen Root, Sarah Goldberg, Anthony Carrigan", "A blade runner must pursue and terminate four replicants who stole a ship in space and have returned to Earth to find their creator.", "Harrison Ford, Rutger Hauer, Sean Young" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "Created", "IsDeleted", "LastLogin", "PasswordHash", "PasswordSalt", "Updated" },
                values: new object[] { new DateTime(2023, 1, 16, 0, 41, 40, 888, DateTimeKind.Local).AddTicks(6276), false, new DateTime(2023, 1, 16, 0, 41, 40, 892, DateTimeKind.Local).AddTicks(1041), new byte[] { 157, 126, 217, 89, 172, 17, 105, 10, 94, 246, 184, 114, 177, 144, 163, 10, 244, 136, 118, 200, 140, 185, 107, 39, 128, 16, 52, 140, 153, 54, 127, 16 }, new byte[] { 61, 205, 183, 105, 25, 244, 230, 155, 169, 255, 112, 168, 98, 234, 158, 203, 130, 126, 199, 206, 75, 29, 220, 173, 74, 245, 190, 150, 157, 240, 73, 85, 138, 133, 17, 58, 249, 31, 45, 186, 5, 223, 184, 104, 147, 85, 60, 57, 228, 49, 138, 130, 16, 44, 31, 12, 230, 183, 45, 201, 141, 99, 106, 130 }, new DateTime(2023, 1, 16, 0, 41, 40, 892, DateTimeKind.Local).AddTicks(602) });
        }
    }
}
