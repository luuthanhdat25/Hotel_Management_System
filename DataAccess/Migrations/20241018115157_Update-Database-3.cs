using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingDetails",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "BookingStatus",
                table: "BookingReservations");

            migrationBuilder.AddColumn<int>(
                name: "BookingDetailId",
                table: "BookingDetails",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingDetails",
                table: "BookingDetails",
                column: "BookingDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_BookingReservationId",
                table: "BookingDetails",
                column: "BookingReservationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingDetails",
                table: "BookingDetails");

            migrationBuilder.DropIndex(
                name: "IX_BookingDetails_BookingReservationId",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "BookingDetailId",
                table: "BookingDetails");

            migrationBuilder.AddColumn<int>(
                name: "BookingStatus",
                table: "BookingReservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingDetails",
                table: "BookingDetails",
                columns: new[] { "BookingReservationId", "RoomId" });
        }
    }
}
