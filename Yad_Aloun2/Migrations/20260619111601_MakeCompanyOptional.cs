using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yad_Aloun2.Migrations
{
    /// <inheritdoc />
    public partial class MakeCompanyOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOpportunity_Donor_Company_Company_ID",
                table: "JobOpportunity");

            migrationBuilder.AlterColumn<int>(
                name: "Company_ID",
                table: "JobOpportunity",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_JobOpportunity_Donor_Company_Company_ID",
                table: "JobOpportunity",
                column: "Company_ID",
                principalTable: "Donor_Company",
                principalColumn: "Company_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOpportunity_Donor_Company_Company_ID",
                table: "JobOpportunity");

            migrationBuilder.AlterColumn<int>(
                name: "Company_ID",
                table: "JobOpportunity",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JobOpportunity_Donor_Company_Company_ID",
                table: "JobOpportunity",
                column: "Company_ID",
                principalTable: "Donor_Company",
                principalColumn: "Company_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
