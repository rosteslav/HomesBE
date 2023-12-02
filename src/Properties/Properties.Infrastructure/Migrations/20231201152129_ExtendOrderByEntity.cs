using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingMarket.Properties.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExtendOrderByEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "related_prop_name",
                schema: "properties",
                table: "OrderBy",
                type: "character varying",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "OrderBy",
                keyColumn: "id",
                keyValue: 1,
                column: "related_prop_name",
                value: "Price");

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "OrderBy",
                keyColumn: "id",
                keyValue: 2,
                column: "related_prop_name",
                value: "CreatedOnLocalTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "related_prop_name",
                schema: "properties",
                table: "OrderBy");
        }
    }
}
