using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileService.Migrations
{
    public partial class UpdatedPersonAwardModelChangedAwardedByToSchoolId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AwardedBy",
                table: "PersonAwards");

            migrationBuilder.AddColumn<Guid>(
                name: "InstituteId",
                table: "PersonAwards",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonAwards_InstituteId",
                table: "PersonAwards",
                column: "InstituteId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonAwards_LookupSchools_InstituteId",
                table: "PersonAwards",
                column: "InstituteId",
                principalTable: "LookupSchools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonAwards_LookupSchools_InstituteId",
                table: "PersonAwards");

            migrationBuilder.DropIndex(
                name: "IX_PersonAwards_InstituteId",
                table: "PersonAwards");

            migrationBuilder.DropColumn(
                name: "InstituteId",
                table: "PersonAwards");

            migrationBuilder.AddColumn<string>(
                name: "AwardedBy",
                table: "PersonAwards",
                type: "text",
                nullable: true);
        }
    }
}
