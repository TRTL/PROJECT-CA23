using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROJECTCA23.Migrations
{
    /// <inheritdoc />
    public partial class userMediaAlternateKeySet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserMedias_UserId",
                table: "UserMedias");

            migrationBuilder.AlterColumn<string>(
                name: "UserRating",
                table: "Reviews",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "UserMediaId",
                table: "Reviews",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserMedias_UserId_MediaId",
                table: "UserMedias",
                columns: new[] { "UserId", "MediaId" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "Created", "LastLogin", "PasswordHash", "PasswordSalt", "Updated" },
                values: new object[] { new DateTime(2023, 1, 16, 0, 41, 40, 888, DateTimeKind.Local).AddTicks(6276), new DateTime(2023, 1, 16, 0, 41, 40, 892, DateTimeKind.Local).AddTicks(1041), new byte[] { 157, 126, 217, 89, 172, 17, 105, 10, 94, 246, 184, 114, 177, 144, 163, 10, 244, 136, 118, 200, 140, 185, 107, 39, 128, 16, 52, 140, 153, 54, 127, 16 }, new byte[] { 61, 205, 183, 105, 25, 244, 230, 155, 169, 255, 112, 168, 98, 234, 158, 203, 130, 126, 199, 206, 75, 29, 220, 173, 74, 245, 190, 150, 157, 240, 73, 85, 138, 133, 17, 58, 249, 31, 45, 186, 5, 223, 184, 104, 147, 85, 60, 57, 228, 49, 138, 130, 16, 44, 31, 12, 230, 183, 45, 201, 141, 99, 106, 130 }, new DateTime(2023, 1, 16, 0, 41, 40, 892, DateTimeKind.Local).AddTicks(602) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserMedias_UserId_MediaId",
                table: "UserMedias");

            migrationBuilder.DropColumn(
                name: "UserMediaId",
                table: "Reviews");

            migrationBuilder.AlterColumn<string>(
                name: "UserRating",
                table: "Reviews",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "Created", "LastLogin", "PasswordHash", "PasswordSalt", "Updated" },
                values: new object[] { new DateTime(2023, 1, 15, 19, 28, 52, 140, DateTimeKind.Local).AddTicks(9197), new DateTime(2023, 1, 15, 19, 28, 52, 143, DateTimeKind.Local).AddTicks(6477), new byte[] { 230, 232, 92, 152, 208, 229, 144, 123, 151, 168, 108, 97, 145, 83, 13, 188, 141, 202, 180, 74, 62, 214, 75, 105, 92, 226, 97, 69, 233, 59, 98, 225 }, new byte[] { 248, 203, 211, 38, 71, 163, 107, 141, 226, 182, 113, 240, 235, 4, 100, 182, 98, 178, 241, 86, 249, 0, 181, 199, 92, 62, 25, 242, 78, 194, 188, 48, 170, 214, 159, 101, 191, 194, 123, 146, 218, 58, 69, 170, 167, 73, 82, 128, 36, 107, 199, 211, 98, 214, 188, 96, 200, 197, 215, 114, 178, 117, 130, 234 }, new DateTime(2023, 1, 15, 19, 28, 52, 143, DateTimeKind.Local).AddTicks(5871) });

            migrationBuilder.CreateIndex(
                name: "IX_UserMedias_UserId",
                table: "UserMedias",
                column: "UserId");
        }
    }
}
