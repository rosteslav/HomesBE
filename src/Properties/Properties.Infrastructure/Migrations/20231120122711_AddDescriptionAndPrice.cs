using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingMarket.Properties.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionAndPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "district",
                schema: "properties",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "type",
                schema: "properties",
                table: "Properties");

            migrationBuilder.AddColumn<string>(
                name: "description",
                schema: "properties",
                table: "Properties",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "price",
                schema: "properties",
                table: "Properties",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                schema: "properties",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "price",
                schema: "properties",
                table: "Properties");

            migrationBuilder.AddColumn<string>(
                name: "district",
                schema: "properties",
                table: "Properties",
                type: "character varying",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "type",
                schema: "properties",
                table: "Properties",
                type: "character varying",
                maxLength: 255,
                nullable: true);
        }
    }
}
