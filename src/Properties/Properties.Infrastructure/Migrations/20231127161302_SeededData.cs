using Microsoft.EntityFrameworkCore.Migrations;

namespace BuildingMarket.Properties.Infrastructure.Migrations
{
    public partial class SeededData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "properties",
                table: "Exposures",
                columns: new[] { "id", "exposure_type" },
                values: new object[,]
                {
                    { 1, "Юг" },
                    { 2, "Изток" },
                    { 3, "Запад" },
                    { 4, "Север" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Exposures",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Exposures",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Exposures",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "properties",
                table: "Exposures",
                keyColumn: "id",
                keyValue: 4);
        }
    }
}
