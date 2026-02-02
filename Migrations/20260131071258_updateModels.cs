using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smart_Dilation_Management.Migrations
{
    /// <inheritdoc />
    public partial class updateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DilationOrder_Doctor_DoctorId",
                table: "DilationOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_User_UserId",
                table: "Doctor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doctor",
                table: "Doctor");

            migrationBuilder.RenameTable(
                name: "Doctor",
                newName: "doctor");

            migrationBuilder.RenameIndex(
                name: "IX_Doctor_UserId",
                table: "doctor",
                newName: "IX_doctor_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_doctor",
                table: "doctor",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    ReceiverId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_DilationOrder_doctor_DoctorId",
                table: "DilationOrder",
                column: "DoctorId",
                principalTable: "doctor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_doctor_User_UserId",
                table: "doctor",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DilationOrder_doctor_DoctorId",
                table: "DilationOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_doctor_User_UserId",
                table: "doctor");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_doctor",
                table: "doctor");

            migrationBuilder.RenameTable(
                name: "doctor",
                newName: "Doctor");

            migrationBuilder.RenameIndex(
                name: "IX_doctor_UserId",
                table: "Doctor",
                newName: "IX_Doctor_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doctor",
                table: "Doctor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DilationOrder_Doctor_DoctorId",
                table: "DilationOrder",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_User_UserId",
                table: "Doctor",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
