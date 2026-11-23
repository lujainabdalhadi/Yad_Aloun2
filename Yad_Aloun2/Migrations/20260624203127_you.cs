using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yad_Aloun2.Migrations
{
    /// <inheritdoc />
    public partial class you : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inquiry_Users_User_Id",
                table: "Inquiry");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Inquiry");

            migrationBuilder.AlterColumn<int>(
                name: "User_Id",
                table: "Inquiry",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ContactNumber",
                table: "HomeProject",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Inquiry_Users_User_Id",
                table: "Inquiry",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "User_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inquiry_Users_User_Id",
                table: "Inquiry");

            migrationBuilder.DropColumn(
                name: "ContactNumber",
                table: "HomeProject");

            migrationBuilder.AlterColumn<int>(
                name: "User_Id",
                table: "Inquiry",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Inquiry",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Inquiry_Users_User_Id",
                table: "Inquiry",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "User_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
