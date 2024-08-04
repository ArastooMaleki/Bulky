using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bulky.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddShoppingCart1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "ID",
                keyValue: new Guid("2fa658a8-5fbe-42c0-843b-0da197b35238"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "ID",
                keyValue: new Guid("45299ac4-c0cd-4f4b-8b3c-a1de08bea3eb"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "ID",
                keyValue: new Guid("da113dd8-545e-43ba-ba70-da5ab1effd68"));

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "ID", "City", "Name", "PhoneNumber", "PostalCode", "State", "StreetAddress" },
                values: new object[,]
                {
                    { new Guid("4ce9642c-ca2d-462c-9dbb-320fdeceefb9"), "VID City", "VivID Books", "7779990000", "66666", "IL", "999 VID St" },
                    { new Guid("5717f76a-78c6-4afa-877d-cfa5531a33a1"), "Tech City", "Tech Solution", "6669990000", "12121", "IL", "123 Tech St" },
                    { new Guid("c8a0a4d7-63ec-46bd-bf1c-b4527bb4f836"), "Lala land", "Readers Club", "1113335555", "99999", "NY", "999 Main St" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "ID",
                keyValue: new Guid("4ce9642c-ca2d-462c-9dbb-320fdeceefb9"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "ID",
                keyValue: new Guid("5717f76a-78c6-4afa-877d-cfa5531a33a1"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "ID",
                keyValue: new Guid("c8a0a4d7-63ec-46bd-bf1c-b4527bb4f836"));

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "ID", "City", "Name", "PhoneNumber", "PostalCode", "State", "StreetAddress" },
                values: new object[,]
                {
                    { new Guid("2fa658a8-5fbe-42c0-843b-0da197b35238"), "Tech City", "Tech Solution", "6669990000", "12121", "IL", "123 Tech St" },
                    { new Guid("45299ac4-c0cd-4f4b-8b3c-a1de08bea3eb"), "Lala land", "Readers Club", "1113335555", "99999", "NY", "999 Main St" },
                    { new Guid("da113dd8-545e-43ba-ba70-da5ab1effd68"), "VID City", "VivID Books", "7779990000", "66666", "IL", "999 VID St" }
                });
        }
    }
}
