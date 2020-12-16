using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileService.Migrations
{
    public partial class AddedProfilePhottToAvatarOnBusinessModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePhoto",
                table: "Businesses");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Businesses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Businesses");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePhoto",
                table: "Businesses",
                type: "text",
                nullable: true);
        }
    }
}
