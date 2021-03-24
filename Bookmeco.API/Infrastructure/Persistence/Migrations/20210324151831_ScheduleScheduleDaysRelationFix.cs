using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class ScheduleScheduleDaysRelationFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCompanies_User_UserId",
                table: "UserCompanies");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleDays_ScheduleId",
                table: "ScheduleDays");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserCompanies",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDays_ScheduleId",
                table: "ScheduleDays",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCompanies_User_UserId",
                table: "UserCompanies",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCompanies_User_UserId",
                table: "UserCompanies");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleDays_ScheduleId",
                table: "ScheduleDays");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserCompanies",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDays_ScheduleId",
                table: "ScheduleDays",
                column: "ScheduleId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCompanies_User_UserId",
                table: "UserCompanies",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
