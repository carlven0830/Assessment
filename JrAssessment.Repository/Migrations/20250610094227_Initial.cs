using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JrAssessment.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblGuest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    GuestName = table.Column<string>(type: "TEXT", nullable: false),
                    GuestEmail = table.Column<string>(type: "TEXT", nullable: false),
                    GuestPhone = table.Column<string>(type: "TEXT", nullable: false),
                    GuestBirthDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    GuestUsername = table.Column<string>(type: "TEXT", nullable: false),
                    GuestPassword = table.Column<string>(type: "TEXT", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsEnabled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblGuest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblRoom",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    RoomNum = table.Column<string>(type: "TEXT", nullable: false),
                    RoomType = table.Column<int>(type: "INTEGER", nullable: false),
                    PricePerNight = table.Column<decimal>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsEnabled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblRoom", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblBooking",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    GuestId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreateBy = table.Column<string>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<string>(type: "TEXT", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsEnabled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblBooking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblBooking_TblGuest_GuestId",
                        column: x => x.GuestId,
                        principalTable: "TblGuest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblBookingRoom",
                columns: table => new
                {
                    TblBookingId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TblRoomId = table.Column<Guid>(type: "TEXT", nullable: false),
                    BookingId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RoomId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblBookingRoom", x => new { x.TblBookingId, x.TblRoomId });
                    table.ForeignKey(
                        name: "FK_TblBookingRoom_TblBooking_TblBookingId",
                        column: x => x.TblBookingId,
                        principalTable: "TblBooking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TblBookingRoom_TblRoom_TblRoomId",
                        column: x => x.TblRoomId,
                        principalTable: "TblRoom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblBooking_GuestId",
                table: "TblBooking",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_TblBookingRoom_TblRoomId",
                table: "TblBookingRoom",
                column: "TblRoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblBookingRoom");

            migrationBuilder.DropTable(
                name: "TblBooking");

            migrationBuilder.DropTable(
                name: "TblRoom");

            migrationBuilder.DropTable(
                name: "TblGuest");
        }
    }
}
