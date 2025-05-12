using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITI.Shipping.Infrastructure.Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01961d25-b4da-7184-a2a8-765486bd4857",
                column: "CreatedAt",
                value: new DateTime(2025, 4, 12, 3, 59, 22, 748, DateTimeKind.Local).AddTicks(2227));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01961d25-b4da-71e9-a488-1b8db232e984",
                column: "CreatedAt",
                value: new DateTime(2025, 4, 12, 3, 59, 22, 748, DateTimeKind.Local).AddTicks(4162));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01961d25-b4da-75a5-a1f4-a7aa10e421ed",
                column: "CreatedAt",
                value: new DateTime(2025, 4, 12, 3, 59, 22, 748, DateTimeKind.Local).AddTicks(4054));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0195d439-9ca1-7873-9c14-a4bc1c201593",
                columns: new[] { "CreatedAt", "Email", "FullName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { new DateTime(2025, 4, 12, 3, 59, 22, 724, DateTimeKind.Local).AddTicks(4662), "Weso430@gmail.com", "Weso Admin", "WESO430@GMAIL.COM", "WESO430@GMAIL.COM", "AQAAAAIAAYagAAAAEE5Z1EcBTN2upkNarqktQnuh9rt8tdGsiDXG/X2qDxJHc6dfNwjFzaaxzOxCWqAEQw==", "Weso430@gmail.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01961d25-b4da-7184-a2a8-765486bd4857",
                column: "CreatedAt",
                value: new DateTime(2025, 4, 10, 3, 21, 34, 366, DateTimeKind.Local).AddTicks(5799));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01961d25-b4da-71e9-a488-1b8db232e984",
                column: "CreatedAt",
                value: new DateTime(2025, 4, 10, 3, 21, 34, 366, DateTimeKind.Local).AddTicks(8368));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01961d25-b4da-75a5-a1f4-a7aa10e421ed",
                column: "CreatedAt",
                value: new DateTime(2025, 4, 10, 3, 21, 34, 366, DateTimeKind.Local).AddTicks(8225));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0195d439-9ca1-7873-9c14-a4bc1c201593",
                columns: new[] { "CreatedAt", "Email", "FullName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { new DateTime(2025, 4, 10, 3, 21, 34, 321, DateTimeKind.Local).AddTicks(2848), "admin@shipping.com", "Shipping Admin", "ADMIN@SHIPPING.COM", "ADMIN@SHIPPING.COM", "AQAAAAIAAYagAAAAEMsgiOfSDL//UsvbhAiiWNkHa5hgDrVvV6yAMpoA3nipvFi+S3FOB3jbUHc1edKIbw==", "admin@shipping.com" });
        }
    }
}
