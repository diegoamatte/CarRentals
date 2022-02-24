using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentals.Migrations
{
    public partial class CreateRentalModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RentalId",
                table: "Cars",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RentStartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RentEndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rentals_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_RentalId",
                table: "Cars",
                column: "RentalId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_ClientId",
                table: "Rentals",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Rentals_RentalId",
                table: "Cars",
                column: "RentalId",
                principalTable: "Rentals",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Rentals_RentalId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Cars_RentalId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "RentalId",
                table: "Cars");
        }
    }
}
