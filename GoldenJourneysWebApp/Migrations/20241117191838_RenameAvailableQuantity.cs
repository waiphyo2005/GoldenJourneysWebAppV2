using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoldenJourneysWebApp.Migrations
{
    /// <inheritdoc />
    public partial class RenameAvailableQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AvailableRoomNumber",
                table: "RoomsAvailability",
                newName: "AvailableQty");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AvailableQty",
                table: "RoomsAvailability",
                newName: "AvailableRoomNumber");
        }
    }
}
