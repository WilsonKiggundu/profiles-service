using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileService.Migrations
{
    public partial class AddedDeveloperProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LookupStacks",
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
                    table.PrimaryKey("PK_LookupStacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonEmploymentHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<string>(nullable: true),
                    DateLastUpdated = table.Column<string>(nullable: true),
                    PersonId = table.Column<Guid>(nullable: false),
                    Company = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    StartYear = table.Column<string>(nullable: true),
                    StartMonth = table.Column<string>(nullable: true),
                    EndYear = table.Column<string>(nullable: true),
                    EndMonth = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonEmploymentHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonProjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<string>(nullable: true),
                    DateLastUpdated = table.Column<string>(nullable: true),
                    PersonId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    StartYear = table.Column<string>(nullable: true),
                    StartMonth = table.Column<string>(nullable: true),
                    EndYear = table.Column<string>(nullable: true),
                    EndMonth = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    TechStack = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    LinkToGitRepo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonProjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeveloperStack",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<string>(nullable: true),
                    DateLastUpdated = table.Column<string>(nullable: true),
                    PersonId = table.Column<Guid>(nullable: false),
                    StackId = table.Column<Guid>(nullable: false),
                    Level = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeveloperStack", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeveloperStack_LookupStacks_StackId",
                        column: x => x.StackId,
                        principalTable: "LookupStacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeveloperStack_StackId",
                table: "DeveloperStack",
                column: "StackId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeveloperStack");

            migrationBuilder.DropTable(
                name: "PersonEmploymentHistory");

            migrationBuilder.DropTable(
                name: "PersonProjects");

            migrationBuilder.DropTable(
                name: "LookupStacks");
        }
    }
}
