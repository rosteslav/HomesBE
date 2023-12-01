using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BuildingMarket.Auth.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PreferencesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Preferences",
                schema: "security",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "character varying", maxLength: 255, nullable: true),
                    purpose = table.Column<string>(type: "character varying", maxLength: 255, nullable: true),
                    region = table.Column<string>(type: "character varying", maxLength: 255, nullable: true),
                    building_type = table.Column<string>(type: "character varying", maxLength: 255, nullable: true),
                    price_higher_end = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preferences", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Preferences",
                schema: "security");
        }
    }
}
