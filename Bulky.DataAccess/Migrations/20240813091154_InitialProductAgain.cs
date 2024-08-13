using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bulky.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialProductAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "ID", "City", "Name", "PhoneNumber", "PostalCode", "State", "StreetAddress" },
                values: new object[,]
                {
                    { new Guid("06413aba-e041-471e-87be-6bb8ed3f2cff"), "VID City", "VivID Books", "7779990000", "66666", "IL", "999 VID St" },
                    { new Guid("90982240-8024-4da4-a53a-70be7e8cb3bd"), "Lala land", "Readers Club", "1113335555", "99999", "NY", "999 Main St" },
                    { new Guid("d06862a9-7e27-43ae-9383-246b05a362d2"), "Tech City", "Tech Solution", "6669990000", "12121", "IL", "123 Tech St" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "ID",
                keyValue: new Guid("06413aba-e041-471e-87be-6bb8ed3f2cff"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "ID",
                keyValue: new Guid("90982240-8024-4da4-a53a-70be7e8cb3bd"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "ID",
                keyValue: new Guid("d06862a9-7e27-43ae-9383-246b05a362d2"));

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "ID", "City", "Name", "PhoneNumber", "PostalCode", "State", "StreetAddress" },
                values: new object[,]
                {
                    { new Guid("8767403d-a25f-44e4-ae79-a3b828171795"), "Lala land", "Readers Club", "1113335555", "99999", "NY", "999 Main St" },
                    { new Guid("91c15a6b-7839-44d0-8631-b88cc8a87005"), "Tech City", "Tech Solution", "6669990000", "12121", "IL", "123 Tech St" },
                    { new Guid("d84e0062-e401-499b-83fa-f2a7f9d4b7ad"), "VID City", "VivID Books", "7779990000", "66666", "IL", "999 VID St" }
                });
        }
    }
}
