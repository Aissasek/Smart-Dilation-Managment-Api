using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smart_Dilation_Management.Migrations
{
    /// <inheritdoc />
    public partial class MakeEyeDropIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
    name: "EyeDropId",
    table: "DilationOrder",
    type: "int",
    nullable: true, // هذا هو التغيير المهم
    oldClrType: typeof(int),
    oldType: "int");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
