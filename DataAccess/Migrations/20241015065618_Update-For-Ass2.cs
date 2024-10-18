using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateForAss2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomTypes_RoomTypeId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "CustomerBookRooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms");

            migrationBuilder.RenameTable(
                name: "Rooms",
                newName: "RoomInformation");

            migrationBuilder.RenameIndex(
                name: "IX_Rooms_RoomTypeId",
                table: "RoomInformation",
                newName: "IX_RoomInformation_RoomTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomInformation",
                table: "RoomInformation",
                column: "RoomId");

            migrationBuilder.CreateTable(
                name: "BookingReservations",
                columns: table => new
                {
                    BookingReservationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "money", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    BookingStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingReservations", x => x.BookingReservationId);
                    table.ForeignKey(
                        name: "FK_BookingReservations_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingDetails",
                columns: table => new
                {
                    BookingReservationId = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingDetails", x => new { x.BookingReservationId, x.RoomId });
                    table.ForeignKey(
                        name: "FK_BookingDetails_BookingReservations_BookingReservationId",
                        column: x => x.BookingReservationId,
                        principalTable: "BookingReservations",
                        principalColumn: "BookingReservationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingDetails_RoomInformation_RoomId",
                        column: x => x.RoomId,
                        principalTable: "RoomInformation",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_RoomId",
                table: "BookingDetails",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingReservations_CustomerId",
                table: "BookingReservations",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomInformation_RoomTypes_RoomTypeId",
                table: "RoomInformation",
                column: "RoomTypeId",
                principalTable: "RoomTypes",
                principalColumn: "RoomTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomInformation_RoomTypes_RoomTypeId",
                table: "RoomInformation");

            migrationBuilder.DropTable(
                name: "BookingDetails");

            migrationBuilder.DropTable(
                name: "BookingReservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomInformation",
                table: "RoomInformation");

            migrationBuilder.RenameTable(
                name: "RoomInformation",
                newName: "Rooms");

            migrationBuilder.RenameIndex(
                name: "IX_RoomInformation_RoomTypeId",
                table: "Rooms",
                newName: "IX_Rooms_RoomTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms",
                column: "RoomId");

            migrationBuilder.CreateTable(
                name: "CustomerBookRooms",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ToDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerBookRooms", x => new { x.CustomerId, x.RoomId });
                    table.ForeignKey(
                        name: "FK_CustomerBookRooms_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerBookRooms_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CustomerBookRooms",
                columns: new[] { "CustomerId", "RoomId", "FromDate", "ToDate" },
                values: new object[,]
                {
                    { 1, 1, new DateOnly(2024, 10, 1), new DateOnly(2024, 10, 20) },
                    { 2, 2, new DateOnly(2024, 10, 10), new DateOnly(2024, 10, 23) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerBookRooms_RoomId",
                table: "CustomerBookRooms",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomTypes_RoomTypeId",
                table: "Rooms",
                column: "RoomTypeId",
                principalTable: "RoomTypes",
                principalColumn: "RoomTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
