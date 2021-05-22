using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileService.Migrations
{
    public partial class AddedEmailToPersonModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Persons",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FreelanceTerms_PersonId",
                table: "FreelanceTerms",
                column: "PersonId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FreelanceTerms_Persons_PersonId",
                table: "FreelanceTerms",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FreelanceTerms_Persons_PersonId",
                table: "FreelanceTerms");

            migrationBuilder.DropIndex(
                name: "IX_FreelanceTerms_PersonId",
                table: "FreelanceTerms");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Persons");
        }
    }
}
