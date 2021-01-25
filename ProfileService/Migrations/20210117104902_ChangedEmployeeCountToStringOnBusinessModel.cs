using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileService.Migrations
{
    public partial class ChangedEmployeeCountToStringOnBusinessModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EmployeeCount",
                table: "Businesses",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Likes_PersonId",
                table: "Likes",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Persons_PersonId",
                table: "Likes",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Persons_PersonId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_PersonId",
                table: "Likes");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeCount",
                table: "Businesses",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
