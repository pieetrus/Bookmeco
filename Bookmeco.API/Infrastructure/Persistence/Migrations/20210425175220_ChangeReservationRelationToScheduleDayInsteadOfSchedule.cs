using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class ChangeReservationRelationToScheduleDayInsteadOfSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Schedules_ScheduleId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "ScheduleDays");

            migrationBuilder.RenameColumn(
                name: "ScheduleId",
                table: "Reservations",
                newName: "ScheduleDayId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_ScheduleId",
                table: "Reservations",
                newName: "IX_Reservations_ScheduleDayId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "ScheduleDays",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BeginTime",
                table: "ScheduleDays",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ScheduleDays_ScheduleDayId",
                table: "Reservations",
                column: "ScheduleDayId",
                principalTable: "ScheduleDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ScheduleDays_ScheduleDayId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "ScheduleDayId",
                table: "Reservations",
                newName: "ScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_ScheduleDayId",
                table: "Reservations",
                newName: "IX_Reservations_ScheduleId");

            migrationBuilder.AlterColumn<int>(
                name: "EndTime",
                table: "ScheduleDays",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "BeginTime",
                table: "ScheduleDays",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "ScheduleDays",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Schedules_ScheduleId",
                table: "Reservations",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
