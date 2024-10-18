using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerFullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerBirthday = table.Column<DateOnly>(type: "date", nullable: false),
                    CustomerStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "RoomTypes",
                columns: table => new
                {
                    RoomTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TypeDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    TypeNote = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypes", x => x.RoomTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RoomDescription = table.Column<string>(type: "nvarchar(220)", maxLength: 220, nullable: false),
                    RoomMaxCapacity = table.Column<int>(type: "int", nullable: false),
                    RoomStatus = table.Column<int>(type: "int", nullable: false),
                    RoomPricePerDate = table.Column<decimal>(type: "money", nullable: false),
                    RoomTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                    table.ForeignKey(
                        name: "FK_Rooms_RoomTypes_RoomTypeId",
                        column: x => x.RoomTypeId,
                        principalTable: "RoomTypes",
                        principalColumn: "RoomTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerBirthday", "CustomerFullName", "CustomerStatus", "EmailAddress", "Password", "Telephone" },
                values: new object[,]
                {
                    { 1, new DateOnly(1990, 1, 1), "Nguyen Van A", 0, "nguyenvana@example.com", "password123", "0123456789" },
                    { 2, new DateOnly(1985, 5, 15), "Tran Thi B", 0, "tranthib@example.com", "password456", "0987654321" }
                });

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "RoomTypeId", "RoomTypeName", "TypeDescription", "TypeNote" },
                values: new object[,]
                {
                    { 1, "Standard", "Basic room with essential amenities", "Includes free breakfast" },
                    { 2, "Deluxe", "Spacious room with additional features", "Sea view, includes free breakfast and gym access" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "RoomId", "RoomDescription", "RoomMaxCapacity", "RoomNumber", "RoomPricePerDate", "RoomStatus", "RoomTypeId" },
                values: new object[,]
                {
                    { 1, "Standard room with queen bed", 2, "101", 100m, 0, 1 },
                    { 2, "Deluxe room with king bed and balcony", 3, "202", 200m, 0, 2 }
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

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomTypeId",
                table: "Rooms",
                column: "RoomTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerBookRooms");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "RoomTypes");
        }
    }
}
