using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shipping.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedIdentityTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedAt", "IsDeleted", "Name", "NormalizedName" },
                values: new object[] { "0195d439-9ca1-7873-9c14-a4bc1c201593", "0195d43b-a808-757b-9c3e-bf90c6091133", new DateTime(2025, 3, 26, 23, 15, 7, 93, DateTimeKind.Local).AddTicks(1125), false, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "BranchId", "CanceledOrder", "CityId", "ConcurrencyStamp", "CreatedAt", "DeductionCompanyFromOrder", "DeductionTypes", "Email", "EmailConfirmed", "FullName", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PickupPrice", "RegionId", "SecurityStamp", "StoreName", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0195d439-9ca1-7873-9c14-a4bc1c201593", 0, null, null, null, null, "0195d43b-a808-757b-9c3e-bf90c6091133", new DateTime(2025, 3, 26, 23, 15, 7, 75, DateTimeKind.Local).AddTicks(9242), null, null, "admin@shipping.com", false, "Shipping Admin", false, false, null, "ADMIN@SHIPPING.COM", "ADMIN@SHIPPING.COM", "AQAAAAIAAYagAAAAENjhApHkXoZCzVm4OAcNZ40XVOPdDiUlQDjgVhhbWsOrkW3EreaeJiAyBoqYUO8GJQ==", null, false, null, null, "0195d43be3f271878cc37be7dfc34361", null, false, "admin@shipping.com" });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "permissions", "Permissions:ViewPermissions", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 2, "permissions", "Permissions:AddPermissions", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 3, "permissions", "Permissions:UpdatePermissions", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 4, "permissions", "Permissions:DeletePermissions", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 5, "permissions", "Settings:ViewSettings", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 6, "permissions", "Settings:AddSettings", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 7, "permissions", "Settings:UpdateSettings", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 8, "permissions", "Settings:DeleteSettings", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 9, "permissions", "Bank:ViewBank", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 10, "permissions", "Bank:AddBank", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 11, "permissions", "Bank:UpdateBank", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 12, "permissions", "Bank:DeleteBank", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 13, "permissions", "MoneySafe:ViewMoneySafe", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 14, "permissions", "MoneySafe:AddMoneySafe", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 15, "permissions", "MoneySafe:UpdateMoneySafe", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 16, "permissions", "MoneySafe:DeleteMoneySafe", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 17, "permissions", "Branches:ViewBranches", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 18, "permissions", "Branches:AddBranches", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 19, "permissions", "Branches:UpdateBranches", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 20, "permissions", "Branches:DeleteBranches", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 21, "permissions", "Employees:ViewEmployees", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 22, "permissions", "Employees:AddEmployees", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 23, "permissions", "Employees:UpdateEmployees", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 24, "permissions", "Employees:DeleteEmployees", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 25, "permissions", "Merchants:ViewMerchants", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 26, "permissions", "Merchants:AddMerchants", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 27, "permissions", "Merchants:UpdateMerchants", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 28, "permissions", "Merchants:DeleteMerchants", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 29, "permissions", "Couriers:ViewCouriers", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 30, "permissions", "Couriers:AddCouriers", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 31, "permissions", "Couriers:UpdateCouriers", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 32, "permissions", "Couriers:DeleteCouriers", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 33, "permissions", "Regions:ViewRegions", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 34, "permissions", "Regions:AddRegions", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 35, "permissions", "Regions:UpdateRegions", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 36, "permissions", "Regions:DeleteRegions", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 37, "permissions", "Cities:ViewCities", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 38, "permissions", "Cities:AddCities", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 39, "permissions", "Cities:UpdateCities", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 40, "permissions", "Cities:DeleteCities", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 41, "permissions", "Orders:ViewOrders", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 42, "permissions", "Orders:AddOrders", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 43, "permissions", "Orders:UpdateOrders", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 44, "permissions", "Orders:DeleteOrders", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 45, "permissions", "OrderReports:ViewOrderReports", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 46, "permissions", "OrderReports:AddOrderReports", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 47, "permissions", "OrderReports:UpdateOrderReports", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 48, "permissions", "OrderReports:DeleteOrderReports", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 49, "permissions", "Accounts:ViewAccounts", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 50, "permissions", "Accounts:AddAccounts", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 51, "permissions", "Accounts:UpdateAccounts", "0195d439-9ca1-7873-9c14-a4bc1c201593" },
                    { 52, "permissions", "Accounts:DeleteAccounts", "0195d439-9ca1-7873-9c14-a4bc1c201593" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "0195d439-9ca1-7873-9c14-a4bc1c201593", "0195d439-9ca1-7873-9c14-a4bc1c201593" });
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
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0195d439-9ca1-7873-9c14-a4bc1c201593", "0195d439-9ca1-7873-9c14-a4bc1c201593" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0195d439-9ca1-7873-9c14-a4bc1c201593");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0195d439-9ca1-7873-9c14-a4bc1c201593");
        }
    }
}
