using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BuildingMarket.Properties.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedNumberOfRoomsToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "security");

            migrationBuilder.AlterColumn<string>(
                name: "number_of_rooms",
                schema: "properties",
                table: "Properties",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "NumberOfRoomsType",
                schema: "properties",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    number_of_rooms_type = table.Column<string>(type: "character varying", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberOfRoomsType", x => x.id);
                });

            migrationBuilder.InsertData(
                schema: "properties",
                table: "NumberOfRoomsType",
                columns: new[] { "id", "number_of_rooms_type" },
                values: new object[,]
                {
                    { 1, "едностаен" },
                    { 2, "двустаен" },
                    { 3, "тристаен" },
                    { 4, "четиристаен" },
                    { 5, "многостаен" },
                    { 6, "мезонет" },
                    { 7, "гараж" },
                    { 8, "склад" },
                    { 9, "таванско помещение" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumberOfRoomsType",
                schema: "properties");

            migrationBuilder.AlterColumn<int>(
                name: "number_of_rooms",
                schema: "properties",
                table: "Properties",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
