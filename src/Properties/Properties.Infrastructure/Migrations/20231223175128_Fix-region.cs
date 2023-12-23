using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingMarket.Properties.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fixregion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 26,
                column: "Region",
                value: "Север");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 26,
                column: "Region",
                value: "Изток");
        }
    }
}
