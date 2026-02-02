using Microsoft.EntityFrameworkCore.Migrations;


#nullable disable

namespace Smart_Dilation_Management.Migrations
{
    /// <inheritdoc />
    public partial class upd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DilationOrder_EyeDrop_EyeDropId",
                table: "DilationOrder");

            migrationBuilder.DropColumn(
                name: "LastDropTime",
                table: "DilationOrder");

            migrationBuilder.AlterColumn<int>(
                name: "EyeDropId",
                table: "DilationOrder",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DropsRequired",
                table: "DilationOrder",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DropsGiven",
                table: "DilationOrder",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DilationOrder_EyeDrop_EyeDropId",
                table: "DilationOrder",
                column: "EyeDropId",
                principalTable: "EyeDrop",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DilationOrder_EyeDrop_EyeDropId",
                table: "DilationOrder");

            migrationBuilder.AlterColumn<int>(
                name: "EyeDropId",
                table: "DilationOrder",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DropsRequired",
                table: "DilationOrder",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DropsGiven",
                table: "DilationOrder",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastDropTime",
                table: "DilationOrder",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DilationOrder_EyeDrop_EyeDropId",
                table: "DilationOrder",
                column: "EyeDropId",
                principalTable: "EyeDrop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
