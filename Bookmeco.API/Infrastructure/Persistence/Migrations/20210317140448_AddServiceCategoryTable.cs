using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class AddServiceCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_SuperCategoryId",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "CategoryCompany");

            migrationBuilder.DropColumn(
                name: "Prize",
                table: "ScheduleDays");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "ScheduleDays");

            migrationBuilder.RenameColumn(
                name: "SuperCategoryId",
                table: "Categories",
                newName: "SuperCompanyCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_SuperCategoryId",
                table: "Categories",
                newName: "IX_Categories_SuperCompanyCategoryId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "ScheduleDays",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AddColumn<float>(
                name: "Prize",
                table: "Reservations",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "ServiceCategoryId",
                table: "Reservations",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Opinions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CompanyContents",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "CompanyContents",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Companies",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Companies",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 60);

            migrationBuilder.CreateTable(
                name: "CompanyCompanyCategory",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompaniesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyCompanyCategory", x => new { x.CategoriesId, x.CompaniesId });
                    table.ForeignKey(
                        name: "FK_CompanyCompanyCategory_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyCompanyCategory_Companies_CompaniesId",
                        column: x => x.CompaniesId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Prize = table.Column<float>(type: "REAL", nullable: false),
                    ServiceDuration = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCategoryUser",
                columns: table => new
                {
                    ServiceCategoriesId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCategoryUser", x => new { x.ServiceCategoriesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ServiceCategoryUser_ServiceCategory_ServiceCategoriesId",
                        column: x => x.ServiceCategoriesId,
                        principalTable: "ServiceCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceCategoryUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ServiceCategoryId",
                table: "Reservations",
                column: "ServiceCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCompanyCategory_CompaniesId",
                table: "CompanyCompanyCategory",
                column: "CompaniesId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCategoryUser_UsersId",
                table: "ServiceCategoryUser",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_SuperCompanyCategoryId",
                table: "Categories",
                column: "SuperCompanyCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ServiceCategory_ServiceCategoryId",
                table: "Reservations",
                column: "ServiceCategoryId",
                principalTable: "ServiceCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_SuperCompanyCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ServiceCategory_ServiceCategoryId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "CompanyCompanyCategory");

            migrationBuilder.DropTable(
                name: "ServiceCategoryUser");

            migrationBuilder.DropTable(
                name: "ServiceCategory");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ServiceCategoryId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Prize",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ServiceCategoryId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "SuperCompanyCategoryId",
                table: "Categories",
                newName: "SuperCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_SuperCompanyCategoryId",
                table: "Categories",
                newName: "IX_Categories_SuperCategoryId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "ScheduleDays",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Prize",
                table: "ScheduleDays",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "Time",
                table: "ScheduleDays",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Opinions",
                type: "TEXT",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CompanyContents",
                type: "TEXT",
                maxLength: 60,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "CompanyContents",
                type: "TEXT",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Companies",
                type: "TEXT",
                maxLength: 60,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Companies",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "TEXT",
                maxLength: 60,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CategoryCompany",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompaniesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryCompany", x => new { x.CategoriesId, x.CompaniesId });
                    table.ForeignKey(
                        name: "FK_CategoryCompany_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryCompany_Companies_CompaniesId",
                        column: x => x.CompaniesId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryCompany_CompaniesId",
                table: "CategoryCompany",
                column: "CompaniesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_SuperCategoryId",
                table: "Categories",
                column: "SuperCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
