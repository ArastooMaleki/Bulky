using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bulky.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CompaniesTableAndAddForiegnKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyID",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "ID", "City", "Name", "PhoneNumber", "PostalCode", "State", "StreetAddress" },
                values: new object[,]
                {
                    { new Guid("09cd4c0d-0273-4624-a690-d50281aa5f75"), "Tech City", "Tech Solution", "6669990000", "12121", "IL", "123 Tech St" },
                    { new Guid("69f556f4-38e6-4e6a-a701-00b34685a403"), "VID City", "VivID Books", "7779990000", "66666", "IL", "999 VID St" },
                    { new Guid("bf1dc305-1a10-44b3-b8e8-8682e1813bf3"), "Lala land", "Readers Club", "1113335555", "99999", "NY", "999 Main St" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CompanyID",
                table: "AspNetUsers",
                column: "CompanyID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyID",
                table: "AspNetUsers",
                column: "CompanyID",
                principalTable: "Companies",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Companies_CompanyID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CompanyID",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "ID",
                keyValue: new Guid("09cd4c0d-0273-4624-a690-d50281aa5f75"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "ID",
                keyValue: new Guid("69f556f4-38e6-4e6a-a701-00b34685a403"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "ID",
                keyValue: new Guid("bf1dc305-1a10-44b3-b8e8-8682e1813bf3"));

            migrationBuilder.DropColumn(
                name: "CompanyID",
                table: "AspNetUsers");
        }
    }
}
