using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileService.Migrations
{
    public partial class UpdatedWellnessModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                schema: "employees",
                table: "Wellness");

            migrationBuilder.DropColumn(
                name: "StartDate",
                schema: "employees",
                table: "Wellness");

            migrationBuilder.AddColumn<string>(
                name: "Details",
                schema: "employees",
                table: "Wellness",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IllnessType",
                schema: "employees",
                table: "Wellness",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Details",
                schema: "employees",
                table: "Wellness");

            migrationBuilder.DropColumn(
                name: "IllnessType",
                schema: "employees",
                table: "Wellness");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                schema: "employees",
                table: "Wellness",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                schema: "employees",
                table: "Wellness",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
