using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileService.Migrations
{
    public partial class UpdatedPersonAwardModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "PersonAwards");

            migrationBuilder.AddColumn<string>(
                name: "Activities",
                table: "PersonAwards",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EndYear",
                table: "PersonAwards",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FieldOfStudy",
                table: "PersonAwards",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "PersonAwards",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StartYear",
                table: "PersonAwards",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "PersonAwards",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activities",
                table: "PersonAwards");

            migrationBuilder.DropColumn(
                name: "EndYear",
                table: "PersonAwards");

            migrationBuilder.DropColumn(
                name: "FieldOfStudy",
                table: "PersonAwards");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "PersonAwards");

            migrationBuilder.DropColumn(
                name: "StartYear",
                table: "PersonAwards");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "PersonAwards");

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "PersonAwards",
                type: "text",
                nullable: true);
        }
    }
}
