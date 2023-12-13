using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DomicWeb.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addCompanyRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "City", "Name", "PhoneNumber", "PostalCode", "State", "StreetAddress" },
                values: new object[,]
                {
                    { 1, "TechCity", "Tech Solution", "039485475", "456", "IL", "123 Tech Street" },
                    { 2, "BooksCity", "DomiBooksShop", "3045967723", "789", "BS", "321 Books Street" },
                    { 3, "PapersCity", "EfficientPaper", "6858437395", "234", "EP", "789 Paper Street" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
