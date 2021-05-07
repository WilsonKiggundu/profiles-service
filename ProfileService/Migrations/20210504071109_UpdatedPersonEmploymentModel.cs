using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileService.Migrations
{
    public partial class UpdatedPersonEmploymentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndMonth",
                table: "PersonEmploymentHistory");

            migrationBuilder.DropColumn(
                name: "EndYear",
                table: "PersonEmploymentHistory");

            migrationBuilder.DropColumn(
                name: "StartMonth",
                table: "PersonEmploymentHistory");

            migrationBuilder.DropColumn(
                name: "StartYear",
                table: "PersonEmploymentHistory");

            migrationBuilder.AddColumn<string>(
                name: "From",
                table: "PersonEmploymentHistory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Until",
                table: "PersonEmploymentHistory",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonProjects_PersonId",
                table: "PersonProjects",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonEmploymentHistory_PersonId",
                table: "PersonEmploymentHistory",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_DeveloperStack_PersonId",
                table: "DeveloperStack",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeveloperStack_Persons_PersonId",
                table: "DeveloperStack",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonEmploymentHistory_Persons_PersonId",
                table: "PersonEmploymentHistory",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonProjects_Persons_PersonId",
                table: "PersonProjects",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeveloperStack_Persons_PersonId",
                table: "DeveloperStack");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonEmploymentHistory_Persons_PersonId",
                table: "PersonEmploymentHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonProjects_Persons_PersonId",
                table: "PersonProjects");

            migrationBuilder.DropIndex(
                name: "IX_PersonProjects_PersonId",
                table: "PersonProjects");

            migrationBuilder.DropIndex(
                name: "IX_PersonEmploymentHistory_PersonId",
                table: "PersonEmploymentHistory");

            migrationBuilder.DropIndex(
                name: "IX_DeveloperStack_PersonId",
                table: "DeveloperStack");

            migrationBuilder.DropColumn(
                name: "From",
                table: "PersonEmploymentHistory");

            migrationBuilder.DropColumn(
                name: "Until",
                table: "PersonEmploymentHistory");

            migrationBuilder.AddColumn<string>(
                name: "EndMonth",
                table: "PersonEmploymentHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EndYear",
                table: "PersonEmploymentHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartMonth",
                table: "PersonEmploymentHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartYear",
                table: "PersonEmploymentHistory",
                type: "text",
                nullable: true);
        }
    }
}
