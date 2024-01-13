using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BuildingMarket.Properties.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NeighbourhoodsUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "region", "Region" },
                values: new object[] { "Център", "Център" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "region", "Region" },
                values: new object[] { "Зона Б-18", "Север" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 3,
                column: "region",
                value: "Света Троица");

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "region", "Region" },
                values: new object[] { "Захарна фабрика", "Север" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "region", "Region" },
                values: new object[] { "Модерно предградие", "Север" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 6,
                columns: new[] { "region", "Region" },
                values: new object[] { "Банишора", "Север" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 7,
                columns: new[] { "region", "Region" },
                values: new object[] { "Фондови жилища", "Север" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 8,
                columns: new[] { "region", "Region" },
                values: new object[] { "Триъгълника", "Север" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 9,
                columns: new[] { "region", "Region" },
                values: new object[] { "Надежда 1", "Север" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 10,
                columns: new[] { "region", "Region" },
                values: new object[] { "Надежда 3", "Север" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 12,
                columns: new[] { "region", "Region" },
                values: new object[] { "Обеля 1", "Север" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 13,
                columns: new[] { "region", "Region" },
                values: new object[] { "Обеля", "Север" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 14,
                columns: new[] { "region", "Region" },
                values: new object[] { "Обеля 2", "Север" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 15,
                columns: new[] { "region", "Region" },
                values: new object[] { "Връбница 2", "Север" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 16,
                columns: new[] { "region", "Region" },
                values: new object[] { "Илиянци", "Север" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 17,
                column: "region",
                value: "Свобода");

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 18,
                columns: new[] { "region", "Region" },
                values: new object[] { "Надежда 4", "Север" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 19,
                columns: new[] { "region", "Region" },
                values: new object[] { "Надежда 2", "Север" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 20,
                columns: new[] { "region", "Region" },
                values: new object[] { "Толстой", "Север" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 21,
                column: "region",
                value: "Военна рампа");

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 22,
                column: "region",
                value: "Орландовци");

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 23,
                columns: new[] { "region", "Region" },
                values: new object[] { "Хаджи Димитър", "Север" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 24,
                columns: new[] { "region", "Region" },
                values: new object[] { "Малашевци", "Север" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 25,
                columns: new[] { "region", "Region" },
                values: new object[] { "Левски В", "Изток" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 26,
                columns: new[] { "region", "Region" },
                values: new object[] { "Левски Г", "Изток" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 27,
                columns: new[] { "region", "Region" },
                values: new object[] { "Левски", "Изток" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 28,
                columns: new[] { "region", "Region" },
                values: new object[] { "Сухата река", "Изток" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 29,
                column: "region",
                value: "Подуяне");

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 30,
                columns: new[] { "region", "Region" },
                values: new object[] { "Яворов", "Изток" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 31,
                columns: new[] { "region", "Region" },
                values: new object[] { "Редута", "Изток" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 32,
                column: "region",
                value: "Христо Ботев");

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 33,
                columns: new[] { "region", "Region" },
                values: new object[] { "Слатина", "Изток" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 34,
                columns: new[] { "region", "Region" },
                values: new object[] { "Гео Милев", "Изток" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 35,
                columns: new[] { "region", "Region" },
                values: new object[] { "Полигона", "Изток" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 36,
                columns: new[] { "region", "Region" },
                values: new object[] { "7-ми 11-ти километър", "Изток" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 37,
                columns: new[] { "region", "Region" },
                values: new object[] { "Дружба 1", "Изток" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 38,
                columns: new[] { "region", "Region" },
                values: new object[] { "Дружба 2", "Изток" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 39,
                columns: new[] { "region", "Region" },
                values: new object[] { "Оборище", "Изток" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 40,
                column: "region",
                value: "Докторски паметник");

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 41,
                columns: new[] { "region", "Region" },
                values: new object[] { "Медицинска академия", "Юг" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 42,
                columns: new[] { "region", "Region" },
                values: new object[] { "Хиподрума", "Юг" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 43,
                column: "region",
                value: "Красно село");

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 44,
                columns: new[] { "region", "Region" },
                values: new object[] { "Бъкстон", "Юг" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 45,
                columns: new[] { "region", "Region" },
                values: new object[] { "Павлово", "Юг" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 46,
                columns: new[] { "region", "Region" },
                values: new object[] { "м-т Гърдова глава", "Юг" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 47,
                columns: new[] { "region", "Region" },
                values: new object[] { "Княжево", "Юг" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 48,
                column: "region",
                value: "в.з.Килиите");

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 49,
                columns: new[] { "region", "Region" },
                values: new object[] { "Иван Вазов", "Юг" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 50,
                columns: new[] { "region", "Region" },
                values: new object[] { "Стрелбище", "Юг" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 51,
                column: "region",
                value: "Белите брези");

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 52,
                column: "region",
                value: "Борово");

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 53,
                columns: new[] { "region", "Region" },
                values: new object[] { "Гоце Делчев", "Юг" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 54,
                columns: new[] { "region", "Region" },
                values: new object[] { "Манастирски ливади", "Юг" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 55,
                columns: new[] { "region", "Region" },
                values: new object[] { "в.з.Беловодски път", "Юг" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 56,
                columns: new[] { "region", "Region" },
                values: new object[] { "Бояна", "Юг" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 57,
                column: "region",
                value: "в.з.Бояна");

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 58,
                columns: new[] { "region", "Region" },
                values: new object[] { "в.з.Киноцентъра 3 част", "Юг" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 59,
                columns: new[] { "region", "Region" },
                values: new object[] { "в.з.Киноцентъра", "Юг" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 60,
                columns: new[] { "region", "Region" },
                values: new object[] { "Драгалевци", "Юг" });

            migrationBuilder.InsertData(
                schema: "properties",
                table: "Neighbourhoods",
                columns: new[] { "id", "region", "Region" },
                values: new object[,]
                {
                    { 11, "Връбница 1", "Север" },
                    { 61, "Кръстова вада", "Юг" },
                    { 62, "Витоша", "Юг" },
                    { 63, "в.з.Симеоново - Драгалевци", "Юг" },
                    { 64, "Симеоново", "Юг" },
                    { 65, "в.з.Малинова долина", "Юг" },
                    { 66, "Лозенец", "Юг" },
                    { 67, "Хладилника", "Юг" },
                    { 68, "Изток", "Юг" },
                    { 69, "Изгрев", "Юг" },
                    { 70, "Дианабад", "Юг" },
                    { 71, "Мусагеница", "Юг" },
                    { 72, "Дървеница", "Юг" },
                    { 73, "Студентски град", "Юг" },
                    { 74, "Малинова долина", "Юг" },
                    { 75, "в.з.Американски колеж", "Юг" },
                    { 76, "Младост 4", "Юг" },
                    { 77, "Младост 3", "Юг" },
                    { 78, "Младост 2", "Юг" },
                    { 79, "Младост 1", "Юг" },
                    { 80, "Младост 1А", "Юг" },
                    { 81, "Експериментален", "Юг" },
                    { 82, "Горубляне", "Юг" },
                    { 83, "Карпузица", "Запад" },
                    { 84, "Овча купел", "Запад" },
                    { 85, "Славия", "Запад" },
                    { 86, "Лагера", "Запад" },
                    { 87, "Люлин 1", "Запад" },
                    { 88, "Люлин 8", "Запад" },
                    { 89, "Люлин 9", "Запад" },
                    { 90, "Люлин 10", "Запад" },
                    { 91, "Гевгелийски", "Запад" },
                    { 92, "Илинден", "Запад" },
                    { 93, "Зона Б-19", "Запад" },
                    { 94, "Зона Б-5", "Запад" },
                    { 95, "Зона Б-5-3", "Запад" },
                    { 96, "Сердика", "Запад" },
                    { 97, "Горна баня", "Запад" },
                    { 98, "Овча купел 2", "Запад" },
                    { 99, "Овча купел 1", "Запад" },
                    { 100, "Факултета", "Запад" },
                    { 101, "Красна поляна 2", "Запад" },
                    { 102, "Красна поляна 3", "Запад" },
                    { 103, "Разсадника", "Запад" },
                    { 104, "Красна поляна 1", "Запад" },
                    { 105, "Западен парк", "Запад" },
                    { 106, "Люлин 7", "Запад" },
                    { 107, "Люлин 6", "Запад" },
                    { 108, "Люлин 5", "Запад" },
                    { 109, "Люлин 4", "Запад" },
                    { 110, "Люлин 3", "Запад" },
                    { 111, "Люлин 2", "Запад" },
                    { 112, "Люлин - център", "Запад" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 112);

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "region", "Region" },
                values: new object[] { "Банишора", "Север" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "region", "Region" },
                values: new object[] { "Белите брези", "Юг" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 3,
                column: "region",
                value: "Бенковски");

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "region", "Region" },
                values: new object[] { "Борово", "Юг" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "region", "Region" },
                values: new object[] { "Бояна", "Юг" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 6,
                columns: new[] { "region", "Region" },
                values: new object[] { "Бъкстон", "Юг" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 7,
                columns: new[] { "region", "Region" },
                values: new object[] { "Витоша", "Юг" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 8,
                columns: new[] { "region", "Region" },
                values: new object[] { "Гевгелийски квартал", "Запад" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 9,
                columns: new[] { "region", "Region" },
                values: new object[] { "Гео Милев", "Изток" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 10,
                columns: new[] { "region", "Region" },
                values: new object[] { "Гоце Делчев", "Юг" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 12,
                columns: new[] { "region", "Region" },
                values: new object[] { "Дианабад", "Юг" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 13,
                columns: new[] { "region", "Region" },
                values: new object[] { "Драгалевци", "Юг" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 14,
                columns: new[] { "region", "Region" },
                values: new object[] { "Дружба", "Изток" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 15,
                columns: new[] { "region", "Region" },
                values: new object[] { "Дървеница", "Юг" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 16,
                columns: new[] { "region", "Region" },
                values: new object[] { "Западен парк", "Запад" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 17,
                column: "region",
                value: "Захарна фабрика");

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 18,
                columns: new[] { "region", "Region" },
                values: new object[] { "Иван Вазов", "Юг" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 19,
                columns: new[] { "region", "Region" },
                values: new object[] { "Изгрев", "Юг" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 20,
                columns: new[] { "region", "Region" },
                values: new object[] { "Изток", "Изток" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 21,
                column: "region",
                value: "Илинден");

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 22,
                column: "region",
                value: "Илиянци");

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 23,
                columns: new[] { "region", "Region" },
                values: new object[] { "Княжево", "Юг" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 24,
                columns: new[] { "region", "Region" },
                values: new object[] { "Красна поляна", "Запад" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 25,
                columns: new[] { "region", "Region" },
                values: new object[] { "Красно село", "Юг" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 26,
                columns: new[] { "region", "Region" },
                values: new object[] { "Крива река", "Юг" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 27,
                columns: new[] { "region", "Region" },
                values: new object[] { "Кръстова вада", "Юг" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 28,
                columns: new[] { "region", "Region" },
                values: new object[] { "Лагерът", "Запад" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 29,
                column: "region",
                value: "Левски");

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 30,
                columns: new[] { "region", "Region" },
                values: new object[] { "Лозенец", "Юг" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 31,
                columns: new[] { "region", "Region" },
                values: new object[] { "Люлин", "Запад" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 32,
                column: "region",
                value: "Малашевци");

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 33,
                columns: new[] { "region", "Region" },
                values: new object[] { "Малинова долина", "Юг" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 34,
                columns: new[] { "region", "Region" },
                values: new object[] { "Манастирски ливади", "Юг" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 35,
                columns: new[] { "region", "Region" },
                values: new object[] { "Младост", "Юг" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 36,
                columns: new[] { "region", "Region" },
                values: new object[] { "Модерно предградие", "Север" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 37,
                columns: new[] { "region", "Region" },
                values: new object[] { "Мусагеница", "Юг" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 38,
                columns: new[] { "region", "Region" },
                values: new object[] { "Надежда", "Север" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 39,
                columns: new[] { "region", "Region" },
                values: new object[] { "Обеля", "Север" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 40,
                column: "region",
                value: "Оборище");

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 41,
                columns: new[] { "region", "Region" },
                values: new object[] { "Овча купел", "Запад" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 42,
                columns: new[] { "region", "Region" },
                values: new object[] { "Орландовци", "Север" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 43,
                column: "region",
                value: "Павлово");

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 44,
                columns: new[] { "region", "Region" },
                values: new object[] { "Подуяне", "Изток" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 45,
                columns: new[] { "region", "Region" },
                values: new object[] { "Разсадника-Коньовица", "Запад" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 46,
                columns: new[] { "region", "Region" },
                values: new object[] { "Редута", "Изток" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 47,
                columns: new[] { "region", "Region" },
                values: new object[] { "Света Троица", "Запад" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 48,
                column: "region",
                value: "Симеоново");

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 49,
                columns: new[] { "region", "Region" },
                values: new object[] { "Славия", "Запад" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 50,
                columns: new[] { "region", "Region" },
                values: new object[] { "Слатина", "Изток" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 51,
                column: "region",
                value: "Стрелбище");

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 52,
                column: "region",
                value: "Студентски град");

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 53,
                columns: new[] { "region", "Region" },
                values: new object[] { "Сухата река", "Изток" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 54,
                columns: new[] { "region", "Region" },
                values: new object[] { "Факултета", "Запад" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 55,
                columns: new[] { "region", "Region" },
                values: new object[] { "Хаджи Димитър", "Изток" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 56,
                columns: new[] { "region", "Region" },
                values: new object[] { "Хиподрумът", "Запад" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 57,
                column: "region",
                value: "Хладилникът");

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 58,
                columns: new[] { "region", "Region" },
                values: new object[] { "Христо Ботев", "Изток" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 59,
                columns: new[] { "region", "Region" },
                values: new object[] { "Център", "Централен" });

            migrationBuilder.UpdateData(
                schema: "properties",
                table: "Neighbourhoods",
                keyColumn: "id",
                keyValue: 60,
                columns: new[] { "region", "Region" },
                values: new object[] { "Яворов", "Изток" });
        }
    }
}
