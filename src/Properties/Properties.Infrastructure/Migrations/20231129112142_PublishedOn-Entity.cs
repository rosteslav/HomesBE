using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BuildingMarket.Properties.Infrastructure.Migrations
{
    public partial class PublishedOnEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PublishedOn",
                schema: "properties",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "character varying", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublishedOn", x => x.id);
                });

            migrationBuilder.InsertData(
                schema: "properties",
                table: "PublishedOn",
                columns: new[] { "id", "description" },
                values: new object[,]
                {
                    { 1, "Днес" },
                    { 2, "Преди 3 дни" },
                    { 3, "Преди седмица" },
                    { 4, "Преди месец" },
                    { 5, "Всички" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PublishedOn",
                schema: "properties");
        }
    }
}
