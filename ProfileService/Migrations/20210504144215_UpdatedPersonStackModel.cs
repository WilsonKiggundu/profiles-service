using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileService.Migrations
{
    public partial class UpdatedPersonStackModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeveloperStack_LookupStacks_StackId",
                table: "DeveloperStack");

            migrationBuilder.DropIndex(
                name: "IX_DeveloperStack_StackId",
                table: "DeveloperStack");

            migrationBuilder.DropColumn(
                name: "StackId",
                table: "DeveloperStack");

            migrationBuilder.AlterColumn<string>(
                name: "Level",
                table: "DeveloperStack",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Stack",
                table: "DeveloperStack",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stack",
                table: "DeveloperStack");

            migrationBuilder.AlterColumn<string>(
                name: "Level",
                table: "DeveloperStack",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<Guid>(
                name: "StackId",
                table: "DeveloperStack",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_DeveloperStack_StackId",
                table: "DeveloperStack",
                column: "StackId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeveloperStack_LookupStacks_StackId",
                table: "DeveloperStack",
                column: "StackId",
                principalTable: "LookupStacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
