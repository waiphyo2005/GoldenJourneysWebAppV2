using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoldenJourneysWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedOriginalRoomQty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OriginalQty",
                table: "RoomsAvailability",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginalQty",
                table: "RoomsAvailability");
        }
    }
}
