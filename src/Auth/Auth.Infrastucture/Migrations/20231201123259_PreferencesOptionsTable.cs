using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BuildingMarket.Auth.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PreferencesOptionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PreferencesOptions",
                schema: "security",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    preference = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferencesOptions", x => x.id);
                });

            migrationBuilder.InsertData(
                schema: "security",
                table: "PreferencesOptions",
                columns: new[] { "id", "preference" },
                values: new object[,]
                {
                    { 1, "За живеене" },
                    { 2, "За инвестиция" },
                    { 3, "Бюджетен" },
                    { 4, "Луксозен" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PreferencesOptions",
                schema: "security");
        }
    }
}
