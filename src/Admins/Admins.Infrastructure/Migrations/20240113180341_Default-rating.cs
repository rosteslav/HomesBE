using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BuildingMarket.Admins.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Defaultrating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "properties",
                table: "NeighbourhoodsRating",
                columns: new[] { "id", "budget", "for_investment", "for_living", "luxury" },
                values: new object[,]
                {
                    { 1, "[]", "[]", "[]", "[]" },
                    { 2, "[]", "[]", "[]", "[]" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "properties",
                table: "NeighbourhoodsRating",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "NeighbourhoodsRating",
                keyColumn: "id",
                keyValue: 2);
        }
    }
}
