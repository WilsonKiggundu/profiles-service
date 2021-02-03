using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileService.Migrations
{
    public partial class AddedSkillModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LookupInterests_Persons_PersonId",
                table: "LookupInterests");

            migrationBuilder.DropIndex(
                name: "IX_LookupInterests_PersonId",
                table: "LookupInterests");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "LookupInterests");

            migrationBuilder.CreateTable(
                name: "LookupSkills",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<string>(nullable: true),
                    DateLastUpdated = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookupSkills", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LookupSkills");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonId",
                table: "LookupInterests",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LookupInterests_PersonId",
                table: "LookupInterests",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_LookupInterests_Persons_PersonId",
                table: "LookupInterests",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
