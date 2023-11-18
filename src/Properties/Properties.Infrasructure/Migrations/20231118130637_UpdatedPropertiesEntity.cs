using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingMarket.Properties.Infrasructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPropertiesEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "total_floors_in_building",
                schema: "properties",
                table: "Properties",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "number_of_rooms",
                schema: "properties",
                table: "Properties",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "floor",
                schema: "properties",
                table: "Properties",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "building_type_id",
                schema: "properties",
                table: "Properties",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "finish_id",
                schema: "properties",
                table: "Properties",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "furnishment_id",
                schema: "properties",
                table: "Properties",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "garage_id",
                schema: "properties",
                table: "Properties",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "heating_id",
                schema: "properties",
                table: "Properties",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "neighborhood_id",
                schema: "properties",
                table: "Properties",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "building_type_id",
                schema: "properties",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "finish_id",
                schema: "properties",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "furnishment_id",
                schema: "properties",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "garage_id",
                schema: "properties",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "heating_id",
                schema: "properties",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "neighborhood_id",
                schema: "properties",
                table: "Properties");

            migrationBuilder.AlterColumn<int>(
                name: "total_floors_in_building",
                schema: "properties",
                table: "Properties",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "number_of_rooms",
                schema: "properties",
                table: "Properties",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "floor",
                schema: "properties",
                table: "Properties",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
