using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileService.Migrations
{
    public partial class AddedAvatarToPersonModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_LookupUploads_UploadId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_UploadId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "UploadId",
                table: "Persons");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Persons",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Persons");

            migrationBuilder.AddColumn<Guid>(
                name: "UploadId",
                table: "Persons",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_UploadId",
                table: "Persons",
                column: "UploadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_LookupUploads_UploadId",
                table: "Persons",
                column: "UploadId",
                principalTable: "LookupUploads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
