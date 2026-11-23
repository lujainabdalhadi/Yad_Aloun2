using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yad_Aloun2.Migrations
{
    /// <inheritdoc />
    public partial class FixServiceItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company_ID",
                table: "ServiceItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Company_ID",
                table: "ServiceItems",
                type: "int",
                nullable: true);
        }
    }
}
