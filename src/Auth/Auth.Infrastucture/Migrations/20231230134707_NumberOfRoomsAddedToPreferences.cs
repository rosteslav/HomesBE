using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingMarket.Auth.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NumberOfRoomsAddedToPreferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "number_of_rooms",
                schema: "security",
                table: "Preferences",
                type: "character varying",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "number_of_rooms",
                schema: "security",
                table: "Preferences");
        }
    }
}
