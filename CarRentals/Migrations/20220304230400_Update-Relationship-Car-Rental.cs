using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentals.Migrations
{
    public partial class UpdateRelationshipCarRental : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Rentals_RentalId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_RentalId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "RentalId",
                table: "Cars");

            migrationBuilder.CreateTable(
                name: "CarRental",
                columns: table => new
                {
                    RentalsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RentedCarsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarRental", x => new { x.RentalsId, x.RentedCarsId });
                    table.ForeignKey(
                        name: "FK_CarRental_Cars_RentedCarsId",
                        column: x => x.RentedCarsId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarRental_Rentals_RentalsId",
                        column: x => x.RentalsId,
                        principalTable: "Rentals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarRental_RentedCarsId",
                table: "CarRental",
                column: "RentedCarsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarRental");

            migrationBuilder.AddColumn<Guid>(
                name: "RentalId",
                table: "Cars",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_RentalId",
                table: "Cars",
                column: "RentalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Rentals_RentalId",
                table: "Cars",
                column: "RentalId",
                principalTable: "Rentals",
                principalColumn: "Id");
        }
    }
}
