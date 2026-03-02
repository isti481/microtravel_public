using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Microtravel.Migrations
{
    /// <inheritdoc />
    public partial class travelIdentifier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TravelIdentifier",
                table: "Travel",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TravelIdentifier",
                table: "Travel");
        }
    }
}
