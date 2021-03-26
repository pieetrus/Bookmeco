using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class UserCompanyFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCompanies_Companies_CompanyId",
                table: "UserCompanies");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCompanies_UserCompanyAccessTypes_AccessTypeId",
                table: "UserCompanies");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "UserCompanies",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AccessTypeId",
                table: "UserCompanies",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCompanies_Companies_CompanyId",
                table: "UserCompanies",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCompanies_UserCompanyAccessTypes_AccessTypeId",
                table: "UserCompanies",
                column: "AccessTypeId",
                principalTable: "UserCompanyAccessTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCompanies_Companies_CompanyId",
                table: "UserCompanies");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCompanies_UserCompanyAccessTypes_AccessTypeId",
                table: "UserCompanies");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "UserCompanies",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "AccessTypeId",
                table: "UserCompanies",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCompanies_Companies_CompanyId",
                table: "UserCompanies",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCompanies_UserCompanyAccessTypes_AccessTypeId",
                table: "UserCompanies",
                column: "AccessTypeId",
                principalTable: "UserCompanyAccessTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
