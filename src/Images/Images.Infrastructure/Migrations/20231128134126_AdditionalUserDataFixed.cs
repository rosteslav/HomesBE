using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingMarket.Images.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalUserDataFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "AdditionalData",
                schema: "security",
                newName: "AdditionalData");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "security");

            migrationBuilder.RenameTable(
                name: "AdditionalData",
                newName: "AdditionalData",
                newSchema: "security");
        }
    }
}
