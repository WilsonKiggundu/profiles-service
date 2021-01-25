using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileService.Migrations
{
    public partial class UpdatedPersonSkillModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Details",
                table: "PersonSkills");

            migrationBuilder.AddColumn<Guid>(
                name: "SkillId",
                table: "PersonSkills",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonSkills_SkillId",
                table: "PersonSkills",
                column: "SkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonSkills_LookupSkills_SkillId",
                table: "PersonSkills",
                column: "SkillId",
                principalTable: "LookupSkills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonSkills_LookupSkills_SkillId",
                table: "PersonSkills");

            migrationBuilder.DropIndex(
                name: "IX_PersonSkills_SkillId",
                table: "PersonSkills");

            migrationBuilder.DropColumn(
                name: "SkillId",
                table: "PersonSkills");

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "PersonSkills",
                type: "text",
                nullable: true);
        }
    }
}
