using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yad_Aloun2.Migrations
{
    /// <inheritdoc />
    public partial class duha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignedCompanyId",
                table: "AssistanceRequest",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedCompanyId",
                table: "AssistanceRequest");
        }
    }
}
