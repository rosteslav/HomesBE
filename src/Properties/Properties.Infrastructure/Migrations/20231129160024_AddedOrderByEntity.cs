using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BuildingMarket.Properties.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedOrderByEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderBy",
                schema: "properties",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_by = table.Column<string>(type: "character varying", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderBy", x => x.id);
                });

            migrationBuilder.InsertData(
                schema: "properties",
                table: "OrderBy",
                columns: new[] { "id", "order_by" },
                values: new object[,]
                {
                    { 1, "Цена" },
                    { 2, "Най-нови" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderBy",
                schema: "properties");
        }
    }
}
