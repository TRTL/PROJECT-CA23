using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROJECTCA23.Migrations
{
    /// <inheritdoc />
    public partial class userMediaStatusModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaStatus",
                table: "UserMedias");

            migrationBuilder.AddColumn<string>(
                name: "UserMediaStatus",
                table: "UserMedias",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "Created", "LastLogin", "PasswordHash", "PasswordSalt", "Updated" },
                values: new object[] { new DateTime(2023, 1, 15, 19, 28, 52, 140, DateTimeKind.Local).AddTicks(9197), new DateTime(2023, 1, 15, 19, 28, 52, 143, DateTimeKind.Local).AddTicks(6477), new byte[] { 230, 232, 92, 152, 208, 229, 144, 123, 151, 168, 108, 97, 145, 83, 13, 188, 141, 202, 180, 74, 62, 214, 75, 105, 92, 226, 97, 69, 233, 59, 98, 225 }, new byte[] { 248, 203, 211, 38, 71, 163, 107, 141, 226, 182, 113, 240, 235, 4, 100, 182, 98, 178, 241, 86, 249, 0, 181, 199, 92, 62, 25, 242, 78, 194, 188, 48, 170, 214, 159, 101, 191, 194, 123, 146, 218, 58, 69, 170, 167, 73, 82, 128, 36, 107, 199, 211, 98, 214, 188, 96, 200, 197, 215, 114, 178, 117, 130, 234 }, new DateTime(2023, 1, 15, 19, 28, 52, 143, DateTimeKind.Local).AddTicks(5871) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserMediaStatus",
                table: "UserMedias");

            migrationBuilder.AddColumn<int>(
                name: "MediaStatus",
                table: "UserMedias",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "Created", "LastLogin", "PasswordHash", "PasswordSalt", "Updated" },
                values: new object[] { new DateTime(2023, 1, 15, 18, 45, 26, 336, DateTimeKind.Local).AddTicks(3741), new DateTime(2023, 1, 15, 18, 45, 26, 338, DateTimeKind.Local).AddTicks(8789), new byte[] { 183, 204, 49, 40, 127, 146, 21, 85, 179, 167, 202, 172, 61, 190, 141, 253, 67, 214, 182, 47, 185, 125, 92, 58, 207, 218, 29, 3, 223, 188, 49, 183 }, new byte[] { 247, 19, 62, 220, 24, 203, 10, 48, 183, 92, 18, 109, 184, 229, 178, 52, 171, 210, 57, 212, 135, 179, 218, 6, 118, 248, 144, 144, 68, 253, 235, 104, 142, 185, 253, 8, 226, 30, 104, 45, 184, 151, 3, 27, 196, 216, 249, 121, 221, 119, 53, 125, 35, 125, 226, 188, 193, 123, 138, 130, 244, 225, 70, 137 }, new DateTime(2023, 1, 15, 18, 45, 26, 338, DateTimeKind.Local).AddTicks(8370) });
        }
    }
}
