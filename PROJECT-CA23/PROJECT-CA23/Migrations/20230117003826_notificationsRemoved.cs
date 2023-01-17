using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROJECTCA23.Migrations
{
    /// <inheritdoc />
    public partial class notificationsRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "Created", "PasswordHash", "PasswordSalt", "Updated" },
                values: new object[] { new DateTime(2023, 1, 17, 2, 38, 25, 584, DateTimeKind.Local).AddTicks(2689), new byte[] { 140, 177, 187, 97, 172, 41, 122, 194, 55, 144, 77, 242, 191, 178, 90, 30, 83, 98, 82, 86, 152, 120, 94, 183, 71, 131, 188, 69, 178, 56, 184, 67 }, new byte[] { 95, 200, 68, 175, 113, 102, 186, 4, 68, 116, 1, 242, 127, 163, 188, 62, 177, 108, 91, 154, 232, 152, 251, 167, 45, 27, 226, 26, 34, 166, 171, 211, 135, 158, 232, 159, 223, 162, 117, 99, 125, 96, 55, 0, 139, 51, 234, 113, 58, 232, 156, 103, 13, 178, 192, 143, 153, 172, 158, 221, 10, 99, 113, 93 }, new DateTime(2023, 1, 17, 2, 38, 25, 587, DateTimeKind.Local).AddTicks(3163) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Shown = table.Column<bool>(type: "INTEGER", nullable: false),
                    Text = table.Column<int>(type: "INTEGER", maxLength: 1000, nullable: false),
                    Title = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "Created", "PasswordHash", "PasswordSalt", "Updated" },
                values: new object[] { new DateTime(2023, 1, 16, 18, 31, 31, 14, DateTimeKind.Local).AddTicks(1094), new byte[] { 142, 86, 160, 57, 248, 0, 147, 16, 27, 190, 251, 38, 107, 80, 200, 64, 148, 58, 154, 79, 87, 241, 70, 88, 223, 73, 35, 138, 8, 169, 135, 251 }, new byte[] { 199, 138, 18, 216, 156, 153, 161, 18, 1, 178, 38, 90, 74, 28, 92, 126, 7, 158, 117, 193, 39, 55, 222, 241, 209, 170, 103, 223, 87, 32, 164, 222, 37, 23, 243, 209, 241, 235, 53, 93, 238, 202, 79, 246, 131, 63, 15, 223, 88, 183, 122, 57, 59, 122, 219, 173, 25, 122, 216, 173, 191, 195, 54, 58 }, new DateTime(2023, 1, 16, 18, 31, 31, 18, DateTimeKind.Local).AddTicks(3056) });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");
        }
    }
}
