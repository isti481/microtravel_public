using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Microtravel.Migrations
{
    /// <inheritdoc />
    public partial class seed_travelType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO TravelType (Name) values" +
                "('Normal')," +
                "('First minute')," +
                "('Last minute')");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
