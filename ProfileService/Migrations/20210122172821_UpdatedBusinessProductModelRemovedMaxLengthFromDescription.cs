using Microsoft.EntityFrameworkCore.Migrations;
using ProfileService.Repositories;

namespace ProfileService.Migrations
{
    public partial class UpdatedBusinessProductModelRemovedMaxLengthFromDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "BusinessProducts",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(280)",
                oldMaxLength: 280,
                oldNullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "BusinessProducts",
                type: "character varying(280)",
                maxLength: 280,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
