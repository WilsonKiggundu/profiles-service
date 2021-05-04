using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileService.Migrations
{
    public partial class UpdatedPersonProjectModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndMonth",
                table: "PersonProjects");

            migrationBuilder.DropColumn(
                name: "EndYear",
                table: "PersonProjects");

            migrationBuilder.DropColumn(
                name: "StartMonth",
                table: "PersonProjects");

            migrationBuilder.DropColumn(
                name: "StartYear",
                table: "PersonProjects");

            migrationBuilder.AddColumn<string>(
                name: "From",
                table: "PersonProjects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Until",
                table: "PersonProjects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "From",
                table: "PersonProjects");

            migrationBuilder.DropColumn(
                name: "Until",
                table: "PersonProjects");

            migrationBuilder.AddColumn<string>(
                name: "EndMonth",
                table: "PersonProjects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EndYear",
                table: "PersonProjects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartMonth",
                table: "PersonProjects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartYear",
                table: "PersonProjects",
                type: "text",
                nullable: true);
        }
    }
}
