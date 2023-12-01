using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingMarket.Properties.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExtendPublishedOnEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "number_of_days",
                schema: "properties",
                table: "PublishedOn",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "PublishedOn",
                keyColumn: "id",
                keyValue: 1,
                column: "number_of_days",
                value: 1);

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "PublishedOn",
                keyColumn: "id",
                keyValue: 2,
                column: "number_of_days",
                value: 3);

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "PublishedOn",
                keyColumn: "id",
                keyValue: 3,
                column: "number_of_days",
                value: 7);

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "PublishedOn",
                keyColumn: "id",
                keyValue: 4,
                column: "number_of_days",
                value: 30);

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "PublishedOn",
                keyColumn: "id",
                keyValue: 5,
                column: "number_of_days",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "number_of_days",
                schema: "properties",
                table: "PublishedOn");
        }
    }
}
