using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class FixServiceCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_SuperCompanyCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyCompanyCategory_Categories_CategoriesId",
                table: "CompanyCompanyCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ServiceCategory_ServiceCategoryId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceCategoryUser_ServiceCategory_ServiceCategoriesId",
                table: "ServiceCategoryUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceCategory",
                table: "ServiceCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "ServiceCategory",
                newName: "ServiceCategories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "CompanyCategories");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_SuperCompanyCategoryId",
                table: "CompanyCategories",
                newName: "IX_CompanyCategories_SuperCompanyCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceCategories",
                table: "ServiceCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyCategories",
                table: "CompanyCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyCategories_CompanyCategories_SuperCompanyCategoryId",
                table: "CompanyCategories",
                column: "SuperCompanyCategoryId",
                principalTable: "CompanyCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyCompanyCategory_CompanyCategories_CategoriesId",
                table: "CompanyCompanyCategory",
                column: "CategoriesId",
                principalTable: "CompanyCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ServiceCategories_ServiceCategoryId",
                table: "Reservations",
                column: "ServiceCategoryId",
                principalTable: "ServiceCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceCategoryUser_ServiceCategories_ServiceCategoriesId",
                table: "ServiceCategoryUser",
                column: "ServiceCategoriesId",
                principalTable: "ServiceCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyCategories_CompanyCategories_SuperCompanyCategoryId",
                table: "CompanyCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyCompanyCategory_CompanyCategories_CategoriesId",
                table: "CompanyCompanyCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ServiceCategories_ServiceCategoryId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceCategoryUser_ServiceCategories_ServiceCategoriesId",
                table: "ServiceCategoryUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceCategories",
                table: "ServiceCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyCategories",
                table: "CompanyCategories");

            migrationBuilder.RenameTable(
                name: "ServiceCategories",
                newName: "ServiceCategory");

            migrationBuilder.RenameTable(
                name: "CompanyCategories",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyCategories_SuperCompanyCategoryId",
                table: "Categories",
                newName: "IX_Categories_SuperCompanyCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceCategory",
                table: "ServiceCategory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_SuperCompanyCategoryId",
                table: "Categories",
                column: "SuperCompanyCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyCompanyCategory_Categories_CategoriesId",
                table: "CompanyCompanyCategory",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ServiceCategory_ServiceCategoryId",
                table: "Reservations",
                column: "ServiceCategoryId",
                principalTable: "ServiceCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceCategoryUser_ServiceCategory_ServiceCategoriesId",
                table: "ServiceCategoryUser",
                column: "ServiceCategoriesId",
                principalTable: "ServiceCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
