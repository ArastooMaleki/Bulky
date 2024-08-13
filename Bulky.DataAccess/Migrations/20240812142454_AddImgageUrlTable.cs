using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bulky.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddImgageUrlTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "ID",
                keyValue: new Guid("1428243c-97ac-4e75-9374-e36c1180107b"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "ID",
                keyValue: new Guid("ccf33ab1-1120-4ca4-af52-857b977e4cb2"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "ID",
                keyValue: new Guid("dcfa18b4-e10b-4ba3-ad56-efcfe3259ef0"));

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "OrderHeaders",
                newName: "ID");

            migrationBuilder.CreateTable(
                name: "ProdcutImages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdcutImages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProdcutImages_Products_ProductID",
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
                    { new Guid("8767403d-a25f-44e4-ae79-a3b828171795"), "Lala land", "Readers Club", "1113335555", "99999", "NY", "999 Main St" },
                    { new Guid("91c15a6b-7839-44d0-8631-b88cc8a87005"), "Tech City", "Tech Solution", "6669990000", "12121", "IL", "123 Tech St" },
                    { new Guid("d84e0062-e401-499b-83fa-f2a7f9d4b7ad"), "VID City", "VivID Books", "7779990000", "66666", "IL", "999 VID St" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProdcutImages_ProductID",
                table: "ProdcutImages",
                column: "ProductID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProdcutImages");

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "ID",
                keyValue: new Guid("8767403d-a25f-44e4-ae79-a3b828171795"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "ID",
                keyValue: new Guid("91c15a6b-7839-44d0-8631-b88cc8a87005"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "ID",
                keyValue: new Guid("d84e0062-e401-499b-83fa-f2a7f9d4b7ad"));

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "OrderHeaders",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "ID", "City", "Name", "PhoneNumber", "PostalCode", "State", "StreetAddress" },
                values: new object[,]
                {
                    { new Guid("1428243c-97ac-4e75-9374-e36c1180107b"), "Lala land", "Readers Club", "1113335555", "99999", "NY", "999 Main St" },
                    { new Guid("ccf33ab1-1120-4ca4-af52-857b977e4cb2"), "VID City", "VivID Books", "7779990000", "66666", "IL", "999 VID St" },
                    { new Guid("dcfa18b4-e10b-4ba3-ad56-efcfe3259ef0"), "Tech City", "Tech Solution", "6669990000", "12121", "IL", "123 Tech St" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 1,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 2,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 3,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 4,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 5,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 6,
                column: "ImageUrl",
                value: null);
        }
    }
}
