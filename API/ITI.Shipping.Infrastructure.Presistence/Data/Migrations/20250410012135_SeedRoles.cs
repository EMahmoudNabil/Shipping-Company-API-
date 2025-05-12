using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ITI.Shipping.Infrastructure.Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedAt", "IsDeleted", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01961d25-b4da-7184-a2a8-765486bd4857", "EAE00686-2608-4516-AD1B-F96CD87C475E", new DateTime(2025, 4, 10, 3, 21, 34, 366, DateTimeKind.Local).AddTicks(5799), false, "Admin", "ADMIN" },
                    { "01961d25-b4da-71e9-a488-1b8db232e984", "1420D50C-F54D-4503-88E8-A2EFA3BD7137", new DateTime(2025, 4, 10, 3, 21, 34, 366, DateTimeKind.Local).AddTicks(8368), false, "Merchant", "MERCHANT" },
                    { "01961d25-b4da-75a5-a1f4-a7aa10e421ed", "386C6E14-D0FD-40FF-80D0-74B419360EF0", new DateTime(2025, 4, 10, 3, 21, 34, 366, DateTimeKind.Local).AddTicks(8225), false, "Courier", "COURIER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "BranchId", "CanceledOrder", "CityId", "ConcurrencyStamp", "CreatedAt", "DeductionCompanyFromOrder", "DeductionTypes", "Email", "EmailConfirmed", "FullName", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PickupPrice", "RegionId", "SecurityStamp", "StoreName", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0195d439-9ca1-7873-9c14-a4bc1c201593", 0, null, null, null, null, "0195d43b-a808-757b-9c3e-bf90c6091133", new DateTime(2025, 4, 10, 3, 21, 34, 321, DateTimeKind.Local).AddTicks(2848), null, null, "admin@shipping.com", false, "Shipping Admin", false, false, null, "ADMIN@SHIPPING.COM", "ADMIN@SHIPPING.COM", "AQAAAAIAAYagAAAAEMsgiOfSDL//UsvbhAiiWNkHa5hgDrVvV6yAMpoA3nipvFi+S3FOB3jbUHc1edKIbw==", null, false, null, null, "0195d43be3f271878cc37be7dfc34361", null, false, "admin@shipping.com" });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "permissions", "Permissions:ViewPermissions", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 2, "permissions", "Permissions:AddPermissions", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 3, "permissions", "Permissions:UpdatePermissions", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 4, "permissions", "Permissions:DeletePermissions", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 5, "permissions", "Settings:ViewSettings", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 6, "permissions", "Settings:AddSettings", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 7, "permissions", "Settings:UpdateSettings", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 8, "permissions", "Settings:DeleteSettings", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 9, "permissions", "Bank:ViewBank", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 10, "permissions", "Bank:AddBank", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 11, "permissions", "Bank:UpdateBank", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 12, "permissions", "Bank:DeleteBank", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 13, "permissions", "MoneySafe:ViewMoneySafe", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 14, "permissions", "MoneySafe:AddMoneySafe", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 15, "permissions", "MoneySafe:UpdateMoneySafe", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 16, "permissions", "MoneySafe:DeleteMoneySafe", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 17, "permissions", "Branches:ViewBranches", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 18, "permissions", "Branches:AddBranches", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 19, "permissions", "Branches:UpdateBranches", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 20, "permissions", "Branches:DeleteBranches", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 21, "permissions", "Employees:ViewEmployees", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 22, "permissions", "Employees:AddEmployees", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 23, "permissions", "Employees:UpdateEmployees", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 24, "permissions", "Employees:DeleteEmployees", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 25, "permissions", "Merchants:ViewMerchants", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 26, "permissions", "Merchants:AddMerchants", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 27, "permissions", "Merchants:UpdateMerchants", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 28, "permissions", "Merchants:DeleteMerchants", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 29, "permissions", "Couriers:ViewCouriers", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 30, "permissions", "Couriers:AddCouriers", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 31, "permissions", "Couriers:UpdateCouriers", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 32, "permissions", "Couriers:DeleteCouriers", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 33, "permissions", "Regions:ViewRegions", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 34, "permissions", "Regions:AddRegions", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 35, "permissions", "Regions:UpdateRegions", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 36, "permissions", "Regions:DeleteRegions", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 37, "permissions", "Cities:ViewCities", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 38, "permissions", "Cities:AddCities", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 39, "permissions", "Cities:UpdateCities", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 40, "permissions", "Cities:DeleteCities", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 41, "permissions", "Orders:ViewOrders", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 42, "permissions", "Orders:AddOrders", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 43, "permissions", "Orders:UpdateOrders", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 44, "permissions", "Orders:DeleteOrders", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 45, "permissions", "OrderReports:ViewOrderReports", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 46, "permissions", "OrderReports:AddOrderReports", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 47, "permissions", "OrderReports:UpdateOrderReports", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 48, "permissions", "OrderReports:DeleteOrderReports", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 49, "permissions", "Accounts:ViewAccounts", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 50, "permissions", "Accounts:AddAccounts", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 51, "permissions", "Accounts:UpdateAccounts", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 52, "permissions", "Accounts:DeleteAccounts", "01961d25-b4da-7184-a2a8-765486bd4857" },
                    { 53, "permissions", "Orders:ViewOrders", "01961d25-b4da-75a5-a1f4-a7aa10e421ed" },
                    { 54, "permissions", "Orders:UpdateOrders", "01961d25-b4da-75a5-a1f4-a7aa10e421ed" },
                    { 105, "permissions", "Orders:ViewOrders", "01961d25-b4da-71e9-a488-1b8db232e984" },
                    { 106, "permissions", "Orders:AddOrders", "01961d25-b4da-71e9-a488-1b8db232e984" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "01961d25-b4da-7184-a2a8-765486bd4857", "0195d439-9ca1-7873-9c14-a4bc1c201593" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "01961d25-b4da-7184-a2a8-765486bd4857", "0195d439-9ca1-7873-9c14-a4bc1c201593" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01961d25-b4da-7184-a2a8-765486bd4857");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01961d25-b4da-71e9-a488-1b8db232e984");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01961d25-b4da-75a5-a1f4-a7aa10e421ed");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0195d439-9ca1-7873-9c14-a4bc1c201593");
        }
    }
}
