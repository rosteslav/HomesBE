using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingMarket.Auth.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrlToAdditionalUserData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "image_url",
                schema: "security",
                table: "AdditionalData",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image_url",
                schema: "security",
                table: "AdditionalData");
        }
    }
}
