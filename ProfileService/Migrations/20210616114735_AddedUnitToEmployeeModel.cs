using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileService.Migrations
{
    public partial class AddedUnitToEmployeeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Unit",
                schema: "employees",
                table: "Employees",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                schema: "employees",
                table: "Employees");
        }
    }
}
