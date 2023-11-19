using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BuildingMarket.Properties.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BuldingOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "building_type",
                schema: "properties",
                table: "Properties",
                type: "character varying",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "finish",
                schema: "properties",
                table: "Properties",
                type: "character varying",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "furnishment",
                schema: "properties",
                table: "Properties",
                type: "character varying",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "garage",
                schema: "properties",
                table: "Properties",
                type: "character varying",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "heating",
                schema: "properties",
                table: "Properties",
                type: "character varying",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "neighbourhood",
                schema: "properties",
                table: "Properties",
                type: "character varying",
                maxLength: 255,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BuildingType",
                schema: "properties",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    building_type = table.Column<string>(type: "character varying", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingType", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Finish",
                schema: "properties",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    finish_type = table.Column<string>(type: "character varying", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finish", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Furnishment",
                schema: "properties",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    furnishment_type = table.Column<string>(type: "character varying", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Furnishment", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Garage",
                schema: "properties",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    garage_type = table.Column<string>(type: "character varying", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garage", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Heating",
                schema: "properties",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    heating_type = table.Column<string>(type: "character varying", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heating", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Neighbourhoods",
                schema: "properties",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Region = table.Column<string>(type: "text", nullable: true),
                    region = table.Column<string>(type: "character varying", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Neighbourhoods", x => x.id);
                });

            migrationBuilder.InsertData(
                schema: "properties",
                table: "BuildingType",
                columns: new[] { "id", "building_type" },
                values: new object[,]
                {
                    { 1, "Тухла" },
                    { 2, "ЕПК" },
                    { 3, "Панел" }
                });

            migrationBuilder.InsertData(
                schema: "properties",
                table: "Finish",
                columns: new[] { "id", "finish_type" },
                values: new object[,]
                {
                    { 1, "Акт 16" },
                    { 2, "Акт 15" },
                    { 3, "Акт 14" },
                    { 4, "В Строеж" },
                    { 5, "На Зелено" }
                });

            migrationBuilder.InsertData(
                schema: "properties",
                table: "Furnishment",
                columns: new[] { "id", "furnishment_type" },
                values: new object[,]
                {
                    { 1, "Необзаведен" },
                    { 2, "Обзаведен" },
                    { 3, "До ключ" }
                });

            migrationBuilder.InsertData(
                schema: "properties",
                table: "Garage",
                columns: new[] { "id", "garage_type" },
                values: new object[,]
                {
                    { 1, "Без" },
                    { 2, "Включен в цената" },
                    { 3, "С възможност" }
                });

            migrationBuilder.InsertData(
                schema: "properties",
                table: "Heating",
                columns: new[] { "id", "heating_type" },
                values: new object[,]
                {
                    { 1, "Без" },
                    { 2, "ТЕЦ" },
                    { 3, "Електричество" }
                });

            migrationBuilder.InsertData(
                schema: "properties",
                table: "Neighbourhoods",
                columns: new[] { "id", "region", "Region" },
                values: new object[,]
                {
                    { 1, "Банишора", "Север" },
                    { 2, "Белите брези", "Юг" },
                    { 3, "Бенковски", "Север" },
                    { 4, "Борово", "Юг" },
                    { 5, "Бояна", "Юг" },
                    { 6, "Бъкстон", "Юг" },
                    { 7, "Витоша", "Юг" },
                    { 8, "Гевгелийски квартал", "Запад" },
                    { 9, "Гео Милев", "Изток" },
                    { 10, "Гоце Делчев", "Юг" },
                    { 12, "Дианабад", "Юг" },
                    { 13, "Драгалевци", "Юг" },
                    { 14, "Дружба", "Изток" },
                    { 15, "Дървеница", "Юг" },
                    { 16, "Западен парк", "Запад" },
                    { 17, "Захарна фабрика", "Север" },
                    { 18, "Иван Вазов", "Юг" },
                    { 19, "Изгрев", "Юг" },
                    { 20, "Изток", "Изток" },
                    { 21, "Илинден", "Север" },
                    { 22, "Илиянци", "Север" },
                    { 23, "Княжево", "Юг" },
                    { 24, "Красна поляна", "Запад" },
                    { 25, "Красно село", "Юг" },
                    { 26, "Крива река", "Юг" },
                    { 27, "Кръстова вада", "Юг" },
                    { 28, "Лагерът", "Запад" },
                    { 29, "Левски", "Изток" },
                    { 30, "Лозенец", "Юг" },
                    { 31, "Люлин", "Запад" },
                    { 32, "Малашевци", "Изток" },
                    { 33, "Малинова долина", "Юг" },
                    { 34, "Манастирски ливади", "Юг" },
                    { 35, "Младост", "Юг" },
                    { 36, "Модерно предградие", "Север" },
                    { 37, "Мусагеница", "Юг" },
                    { 38, "Надежда", "Север" },
                    { 39, "Обеля", "Север" },
                    { 40, "Оборище", "Изток" },
                    { 41, "Овча купел", "Запад" },
                    { 42, "Орландовци", "Север" },
                    { 43, "Павлово", "Юг" },
                    { 44, "Подуяне", "Изток" },
                    { 45, "Разсадника-Коньовица", "Запад" },
                    { 46, "Редута", "Изток" },
                    { 47, "Света Троица", "Запад" },
                    { 48, "Симеоново", "Юг" },
                    { 49, "Славия", "Запад" },
                    { 50, "Слатина", "Изток" },
                    { 51, "Стрелбище", "Юг" },
                    { 52, "Студентски град", "Юг" },
                    { 53, "Сухата река", "Изток" },
                    { 54, "Факултета", "Запад" },
                    { 55, "Хаджи Димитър", "Изток" },
                    { 56, "Хиподрумът", "Запад" },
                    { 57, "Хладилникът", "Юг" },
                    { 58, "Христо Ботев", "Изток" },
                    { 59, "Център", "Централен" },
                    { 60, "Яворов", "Изток" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuildingType",
                schema: "properties");

            migrationBuilder.DropTable(
                name: "Finish",
                schema: "properties");

            migrationBuilder.DropTable(
                name: "Furnishment",
                schema: "properties");

            migrationBuilder.DropTable(
                name: "Garage",
                schema: "properties");

            migrationBuilder.DropTable(
                name: "Heating",
                schema: "properties");

            migrationBuilder.DropTable(
                name: "Neighbourhoods",
                schema: "properties");

            migrationBuilder.DropColumn(
                name: "building_type",
                schema: "properties",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "finish",
                schema: "properties",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "furnishment",
                schema: "properties",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "garage",
                schema: "properties",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "heating",
                schema: "properties",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "neighbourhood",
                schema: "properties",
                table: "Properties");

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
    }
}
