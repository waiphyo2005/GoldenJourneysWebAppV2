using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoldenJourneysWebApp.Migrations
{
    /// <inheritdoc />
    public partial class Final2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "FromDate",
                table: "BookingHotels",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "ToDate",
                table: "BookingHotels",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromDate",
                table: "BookingHotels");

            migrationBuilder.DropColumn(
                name: "ToDate",
                table: "BookingHotels");
        }
    }
}
