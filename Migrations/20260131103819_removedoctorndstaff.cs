using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smart_Dilation_Management.Migrations
{
    /// <inheritdoc />
    public partial class removedoctorndstaff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DilationOrder_doctor_DoctorId",
                table: "DilationOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_DoseLog_Staff_StaffId",
                table: "DoseLog");

            migrationBuilder.DropTable(
                name: "doctor");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.AddColumn<bool>(
                name: "IsFree",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_DilationOrder_User_DoctorId",
                table: "DilationOrder",
                column: "DoctorId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DoseLog_User_StaffId",
                table: "DoseLog",
                column: "StaffId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DilationOrder_User_DoctorId",
                table: "DilationOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_DoseLog_User_StaffId",
                table: "DoseLog");

            migrationBuilder.DropColumn(
                name: "IsFree",
                table: "User");

            migrationBuilder.CreateTable(
                name: "doctor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsFree = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doctor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_doctor_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Staff_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_doctor_UserId",
                table: "doctor",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_UserId",
                table: "Staff",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DilationOrder_doctor_DoctorId",
                table: "DilationOrder",
                column: "DoctorId",
                principalTable: "doctor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DoseLog_Staff_StaffId",
                table: "DoseLog",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id");
        }
    }
}
