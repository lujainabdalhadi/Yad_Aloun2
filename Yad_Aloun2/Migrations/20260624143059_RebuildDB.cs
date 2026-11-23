using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yad_Aloun2.Migrations
{
    /// <inheritdoc />
    public partial class RebuildDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOpportunity_Donor_Company_Company_ID",
                table: "JobOpportunity");

            migrationBuilder.RenameColumn(
                name: "Company_ID",
                table: "JobOpportunity",
                newName: "Admin_Id");

            migrationBuilder.RenameIndex(
                name: "IX_JobOpportunity_Company_ID",
                table: "JobOpportunity",
                newName: "IX_JobOpportunity_Admin_Id");

            migrationBuilder.AddColumn<int>(
                name: "Admin_Id",
                table: "UserFile",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Inquiry",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "User_Id",
                table: "Inquiry",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Admin_Id",
                table: "Company_Files",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Company_ID",
                table: "AssistanceRequest",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "User_Id",
                table: "AssistanceRequest",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserFile_Admin_Id",
                table: "UserFile",
                column: "Admin_Id");

            migrationBuilder.CreateIndex(
                name: "IX_usaglog_CompanyId",
                table: "usaglog",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiry_User_Id",
                table: "Inquiry",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Company_Files_Admin_Id",
                table: "Company_Files",
                column: "Admin_Id");

            migrationBuilder.CreateIndex(
                name: "IX_AssistanceRequest_Company_ID",
                table: "AssistanceRequest",
                column: "Company_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AssistanceRequest_User_Id",
                table: "AssistanceRequest",
                column: "User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssistanceRequest_Donor_Company_Company_ID",
                table: "AssistanceRequest",
                column: "Company_ID",
                principalTable: "Donor_Company",
                principalColumn: "Company_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AssistanceRequest_Users_User_Id",
                table: "AssistanceRequest",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Files_Admins_Admin_Id",
                table: "Company_Files",
                column: "Admin_Id",
                principalTable: "Admins",
                principalColumn: "Admin_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inquiry_Users_User_Id",
                table: "Inquiry",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "User_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobOpportunity_Admins_Admin_Id",
                table: "JobOpportunity",
                column: "Admin_Id",
                principalTable: "Admins",
                principalColumn: "Admin_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_usaglog_Donor_Company_CompanyId",
                table: "usaglog",
                column: "CompanyId",
                principalTable: "Donor_Company",
                principalColumn: "Company_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFile_Admins_Admin_Id",
                table: "UserFile",
                column: "Admin_Id",
                principalTable: "Admins",
                principalColumn: "Admin_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssistanceRequest_Donor_Company_Company_ID",
                table: "AssistanceRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_AssistanceRequest_Users_User_Id",
                table: "AssistanceRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_Company_Files_Admins_Admin_Id",
                table: "Company_Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Inquiry_Users_User_Id",
                table: "Inquiry");

            migrationBuilder.DropForeignKey(
                name: "FK_JobOpportunity_Admins_Admin_Id",
                table: "JobOpportunity");

            migrationBuilder.DropForeignKey(
                name: "FK_usaglog_Donor_Company_CompanyId",
                table: "usaglog");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFile_Admins_Admin_Id",
                table: "UserFile");

            migrationBuilder.DropIndex(
                name: "IX_UserFile_Admin_Id",
                table: "UserFile");

            migrationBuilder.DropIndex(
                name: "IX_usaglog_CompanyId",
                table: "usaglog");

            migrationBuilder.DropIndex(
                name: "IX_Inquiry_User_Id",
                table: "Inquiry");

            migrationBuilder.DropIndex(
                name: "IX_Company_Files_Admin_Id",
                table: "Company_Files");

            migrationBuilder.DropIndex(
                name: "IX_AssistanceRequest_Company_ID",
                table: "AssistanceRequest");

            migrationBuilder.DropIndex(
                name: "IX_AssistanceRequest_User_Id",
                table: "AssistanceRequest");

            migrationBuilder.DropColumn(
                name: "Admin_Id",
                table: "UserFile");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Inquiry");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Inquiry");

            migrationBuilder.DropColumn(
                name: "Admin_Id",
                table: "Company_Files");

            migrationBuilder.DropColumn(
                name: "Company_ID",
                table: "AssistanceRequest");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "AssistanceRequest");

            migrationBuilder.RenameColumn(
                name: "Admin_Id",
                table: "JobOpportunity",
                newName: "Company_ID");

            migrationBuilder.RenameIndex(
                name: "IX_JobOpportunity_Admin_Id",
                table: "JobOpportunity",
                newName: "IX_JobOpportunity_Company_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_JobOpportunity_Donor_Company_Company_ID",
                table: "JobOpportunity",
                column: "Company_ID",
                principalTable: "Donor_Company",
                principalColumn: "Company_ID");
        }
    }
}
