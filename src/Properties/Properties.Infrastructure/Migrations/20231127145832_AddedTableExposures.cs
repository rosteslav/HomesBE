using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BuildingMarket.Properties.Infrastructure.Migrations
{
    public partial class AddedTableExposures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "exposure",
                schema: "properties",
                table: "Properties",
                type: "character varying",
                maxLength: 255,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Exposures",
                schema: "properties",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    exposure_type = table.Column<string>(type: "character varying", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exposures", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exposures",
                schema: "properties");

            migrationBuilder.DropColumn(
                name: "exposure",
                schema: "properties",
                table: "Properties");
        }
    }
}
