using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shipping.Api.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0195d439-9ca1-7873-9c14-a4bc1c201593",
                column: "CreatedAt",
                value: new DateTime(2025, 3, 27, 10, 21, 4, 689, DateTimeKind.Local).AddTicks(4606));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0195d439-9ca1-7873-9c14-a4bc1c201593",
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 3, 27, 10, 21, 4, 659, DateTimeKind.Local).AddTicks(9845), "AQAAAAIAAYagAAAAEOuMA+X/oySuPiMFA8J0mG7+OBgnMKMjA8tLuqnDYXN3VxV3GbAJc0nZj0uYzjeAug==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0195d439-9ca1-7873-9c14-a4bc1c201593",
                column: "CreatedAt",
                value: new DateTime(2025, 3, 27, 10, 18, 46, 621, DateTimeKind.Local).AddTicks(1792));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0195d439-9ca1-7873-9c14-a4bc1c201593",
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 3, 27, 10, 18, 46, 591, DateTimeKind.Local).AddTicks(9820), "AQAAAAIAAYagAAAAEI5z7BwtVFhurk8CCFMIUSxksxepW231Ii5LFcgw/OEo+aPRpoDRBSZ01JClEp20dw==" });
        }
    }
}
