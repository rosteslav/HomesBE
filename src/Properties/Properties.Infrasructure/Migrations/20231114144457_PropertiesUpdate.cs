using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingMarket.Properties.Infrasructure.Migrations
{
    /// <inheritdoc />
    public partial class PropertiesUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                schema: "properties",
                table: "Properties");

            migrationBuilder.AlterColumn<string>(
                name: "seller_id",
                schema: "properties",
                table: "Properties",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "district",
                schema: "properties",
                table: "Properties",
                type: "character varying",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<short>(
                name: "floor",
                schema: "properties",
                table: "Properties",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "number of rooms",
                schema: "properties",
                table: "Properties",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<float>(
                name: "space",
                schema: "properties",
                table: "Properties",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<short>(
                name: "total floors in building",
                schema: "properties",
                table: "Properties",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<string>(
                name: "type",
                schema: "properties",
                table: "Properties",
                type: "character varying",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "district",
                schema: "properties",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "floor",
                schema: "properties",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "number of rooms",
                schema: "properties",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "space",
                schema: "properties",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "total floors in building",
                schema: "properties",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "type",
                schema: "properties",
                table: "Properties");

            migrationBuilder.AlterColumn<string>(
                name: "seller_id",
                schema: "properties",
                table: "Properties",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "name",
                schema: "properties",
                table: "Properties",
                type: "character varying",
                maxLength: 255,
                nullable: true);
        }
    }
}
