using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bulky.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddShoppingCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "shoppingCarts",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shoppingCarts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_shoppingCarts_AspNetUsers_ApplicationUserID",
                        column: x => x.ApplicationUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_shoppingCarts_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "ID", "City", "Name", "PhoneNumber", "PostalCode", "State", "StreetAddress" },
                values: new object[,]
                {
                    { new Guid("2fa658a8-5fbe-42c0-843b-0da197b35238"), "Tech City", "Tech Solution", "6669990000", "12121", "IL", "123 Tech St" },
                    { new Guid("45299ac4-c0cd-4f4b-8b3c-a1de08bea3eb"), "Lala land", "Readers Club", "1113335555", "99999", "NY", "999 Main St" },
                    { new Guid("da113dd8-545e-43ba-ba70-da5ab1effd68"), "VID City", "VivID Books", "7779990000", "66666", "IL", "999 VID St" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_shoppingCarts_ApplicationUserID",
                table: "shoppingCarts",
                column: "ApplicationUserID");

            migrationBuilder.CreateIndex(
                name: "IX_shoppingCarts_ProductID",
                table: "shoppingCarts",
                column: "ProductID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "shoppingCarts");

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
                    { new Guid("09cd4c0d-0273-4624-a690-d50281aa5f75"), "Tech City", "Tech Solution", "6669990000", "12121", "IL", "123 Tech St" },
                    { new Guid("69f556f4-38e6-4e6a-a701-00b34685a403"), "VID City", "VivID Books", "7779990000", "66666", "IL", "999 VID St" },
                    { new Guid("bf1dc305-1a10-44b3-b8e8-8682e1813bf3"), "Lala land", "Readers Club", "1113335555", "99999", "NY", "999 Main St" }
                });
        }
    }
}
