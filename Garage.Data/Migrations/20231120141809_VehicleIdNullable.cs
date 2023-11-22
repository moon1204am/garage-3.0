using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Garage.Data.Migrations
{
    /// <inheritdoc />
    public partial class VehicleIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSpot_Vehicle_VehicleId",
                table: "ParkingSpot");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "ParkingSpot",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSpot_Vehicle_VehicleId",
                table: "ParkingSpot",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSpot_Vehicle_VehicleId",
                table: "ParkingSpot");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "ParkingSpot",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSpot_Vehicle_VehicleId",
                table: "ParkingSpot",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "VehicleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
