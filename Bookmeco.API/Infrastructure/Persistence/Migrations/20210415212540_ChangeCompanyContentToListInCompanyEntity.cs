using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class ChangeCompanyContentToListInCompanyEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CompanyContents_CompanyId",
                table: "CompanyContents");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContents_CompanyId",
                table: "CompanyContents",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CompanyContents_CompanyId",
                table: "CompanyContents");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContents_CompanyId",
                table: "CompanyContents",
                column: "CompanyId",
                unique: true);
        }
    }
}
