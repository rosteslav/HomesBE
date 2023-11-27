using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BuildingMarket.Properties.Infrastructure.Migrations
{
    public partial class ExposureTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Exposure",
                schema: "properties",
                table: "Properties",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Exposures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exposures", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exposures");

            migrationBuilder.DropColumn(
                name: "Exposure",
                schema: "properties",
                table: "Properties");
        }
    }
}
