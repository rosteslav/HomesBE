using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingMarket.Properties.Infrasructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangesMadeForPR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "total floors in building",
                schema: "properties",
                table: "Properties",
                newName: "total_floors_in_building");

            migrationBuilder.RenameColumn(
                name: "number of rooms",
                schema: "properties",
                table: "Properties",
                newName: "number_of_rooms");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                schema: "properties",
                table: "Properties",
                type: "character varying",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "seller_id",
                schema: "properties",
                table: "Properties",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "floor",
                schema: "properties",
                table: "Properties",
                type: "int",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<string>(
                name: "district",
                schema: "properties",
                table: "Properties",
                type: "character varying",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<int>(
                name: "total_floors_in_building",
                schema: "properties",
                table: "Properties",
                type: "int",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<int>(
                name: "number_of_rooms",
                schema: "properties",
                table: "Properties",
                type: "int",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "total_floors_in_building",
                schema: "properties",
                table: "Properties",
                newName: "total floors in building");

            migrationBuilder.RenameColumn(
                name: "number_of_rooms",
                schema: "properties",
                table: "Properties",
                newName: "number of rooms");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                schema: "properties",
                table: "Properties",
                type: "character varying",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying",
                oldMaxLength: 255,
                oldNullable: true);

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

            migrationBuilder.AlterColumn<short>(
                name: "floor",
                schema: "properties",
                table: "Properties",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "district",
                schema: "properties",
                table: "Properties",
                type: "character varying",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "total floors in building",
                schema: "properties",
                table: "Properties",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<short>(
                name: "number of rooms",
                schema: "properties",
                table: "Properties",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
