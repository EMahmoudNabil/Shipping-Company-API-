using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shipping.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingDecimalValidtions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecialCityCost_AspNetUsers_MerchantId",
                table: "SpecialCityCost");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialCityCost_CitySettings_CitySettingId",
                table: "SpecialCityCost");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialCourierRegion_AspNetUsers_CourierId",
                table: "SpecialCourierRegion");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialCourierRegion_Regions_RegionId",
                table: "SpecialCourierRegion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpecialCourierRegion",
                table: "SpecialCourierRegion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpecialCityCost",
                table: "SpecialCityCost");

            migrationBuilder.RenameTable(
                name: "SpecialCourierRegion",
                newName: "GetSpecialCourierRegions");

            migrationBuilder.RenameTable(
                name: "SpecialCityCost",
                newName: "SpecialCityCosts");

            migrationBuilder.RenameIndex(
                name: "IX_SpecialCourierRegion_RegionId",
                table: "GetSpecialCourierRegions",
                newName: "IX_GetSpecialCourierRegions_RegionId");

            migrationBuilder.RenameIndex(
                name: "IX_SpecialCourierRegion_CourierId",
                table: "GetSpecialCourierRegions",
                newName: "IX_GetSpecialCourierRegions_CourierId");

            migrationBuilder.RenameIndex(
                name: "IX_SpecialCityCost_MerchantId",
                table: "SpecialCityCosts",
                newName: "IX_SpecialCityCosts_MerchantId");

            migrationBuilder.RenameIndex(
                name: "IX_SpecialCityCost_CitySettingId",
                table: "SpecialCityCosts",
                newName: "IX_SpecialCityCosts_CitySettingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GetSpecialCourierRegions",
                table: "GetSpecialCourierRegions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpecialCityCosts",
                table: "SpecialCityCosts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GetSpecialCourierRegions_AspNetUsers_CourierId",
                table: "GetSpecialCourierRegions",
                column: "CourierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GetSpecialCourierRegions_Regions_RegionId",
                table: "GetSpecialCourierRegions",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialCityCosts_AspNetUsers_MerchantId",
                table: "SpecialCityCosts",
                column: "MerchantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialCityCosts_CitySettings_CitySettingId",
                table: "SpecialCityCosts",
                column: "CitySettingId",
                principalTable: "CitySettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GetSpecialCourierRegions_AspNetUsers_CourierId",
                table: "GetSpecialCourierRegions");

            migrationBuilder.DropForeignKey(
                name: "FK_GetSpecialCourierRegions_Regions_RegionId",
                table: "GetSpecialCourierRegions");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialCityCosts_AspNetUsers_MerchantId",
                table: "SpecialCityCosts");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialCityCosts_CitySettings_CitySettingId",
                table: "SpecialCityCosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpecialCityCosts",
                table: "SpecialCityCosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GetSpecialCourierRegions",
                table: "GetSpecialCourierRegions");

            migrationBuilder.RenameTable(
                name: "SpecialCityCosts",
                newName: "SpecialCityCost");

            migrationBuilder.RenameTable(
                name: "GetSpecialCourierRegions",
                newName: "SpecialCourierRegion");

            migrationBuilder.RenameIndex(
                name: "IX_SpecialCityCosts_MerchantId",
                table: "SpecialCityCost",
                newName: "IX_SpecialCityCost_MerchantId");

            migrationBuilder.RenameIndex(
                name: "IX_SpecialCityCosts_CitySettingId",
                table: "SpecialCityCost",
                newName: "IX_SpecialCityCost_CitySettingId");

            migrationBuilder.RenameIndex(
                name: "IX_GetSpecialCourierRegions_RegionId",
                table: "SpecialCourierRegion",
                newName: "IX_SpecialCourierRegion_RegionId");

            migrationBuilder.RenameIndex(
                name: "IX_GetSpecialCourierRegions_CourierId",
                table: "SpecialCourierRegion",
                newName: "IX_SpecialCourierRegion_CourierId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpecialCityCost",
                table: "SpecialCityCost",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpecialCourierRegion",
                table: "SpecialCourierRegion",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialCityCost_AspNetUsers_MerchantId",
                table: "SpecialCityCost",
                column: "MerchantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialCityCost_CitySettings_CitySettingId",
                table: "SpecialCityCost",
                column: "CitySettingId",
                principalTable: "CitySettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialCourierRegion_AspNetUsers_CourierId",
                table: "SpecialCourierRegion",
                column: "CourierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialCourierRegion_Regions_RegionId",
                table: "SpecialCourierRegion",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
