using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BuildingMarket.Admins.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNeighbourhoodsRatingEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "properties");

            migrationBuilder.CreateTable(
                name: "NeighbourhoodsRating",
                schema: "properties",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    for_living = table.Column<string>(type: "character varying", maxLength: 255, nullable: false),
                    for_investment = table.Column<string>(type: "character varying", maxLength: 255, nullable: false),
                    budget = table.Column<string>(type: "character varying", maxLength: 255, nullable: false),
                    luxury = table.Column<string>(type: "character varying", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NeighbourhoodsRating", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NeighbourhoodsRating",
                schema: "properties");
        }
    }
}
