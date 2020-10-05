using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileService.Migrations
{
    public partial class ModifiedInterestModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LookupInterests_LookupUploads_UploadId",
                table: "LookupInterests");

            migrationBuilder.DropForeignKey(
                name: "FK_LookupNeeds_LookupUploads_UploadId",
                table: "LookupNeeds");

            migrationBuilder.DropIndex(
                name: "IX_LookupNeeds_UploadId",
                table: "LookupNeeds");

            migrationBuilder.DropIndex(
                name: "IX_LookupInterests_UploadId",
                table: "LookupInterests");

            migrationBuilder.DropColumn(
                name: "UploadId",
                table: "LookupNeeds");

            migrationBuilder.DropColumn(
                name: "UploadId",
                table: "LookupInterests");

            migrationBuilder.AddColumn<Guid>(
                name: "IconId",
                table: "LookupNeeds",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IconId",
                table: "LookupInterests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LookupNeeds_IconId",
                table: "LookupNeeds",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_LookupInterests_IconId",
                table: "LookupInterests",
                column: "IconId");

            migrationBuilder.AddForeignKey(
                name: "FK_LookupInterests_LookupUploads_IconId",
                table: "LookupInterests",
                column: "IconId",
                principalTable: "LookupUploads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LookupNeeds_LookupUploads_IconId",
                table: "LookupNeeds",
                column: "IconId",
                principalTable: "LookupUploads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LookupInterests_LookupUploads_IconId",
                table: "LookupInterests");

            migrationBuilder.DropForeignKey(
                name: "FK_LookupNeeds_LookupUploads_IconId",
                table: "LookupNeeds");

            migrationBuilder.DropIndex(
                name: "IX_LookupNeeds_IconId",
                table: "LookupNeeds");

            migrationBuilder.DropIndex(
                name: "IX_LookupInterests_IconId",
                table: "LookupInterests");

            migrationBuilder.DropColumn(
                name: "IconId",
                table: "LookupNeeds");

            migrationBuilder.DropColumn(
                name: "IconId",
                table: "LookupInterests");

            migrationBuilder.AddColumn<Guid>(
                name: "UploadId",
                table: "LookupNeeds",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UploadId",
                table: "LookupInterests",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LookupNeeds_UploadId",
                table: "LookupNeeds",
                column: "UploadId");

            migrationBuilder.CreateIndex(
                name: "IX_LookupInterests_UploadId",
                table: "LookupInterests",
                column: "UploadId");

            migrationBuilder.AddForeignKey(
                name: "FK_LookupInterests_LookupUploads_UploadId",
                table: "LookupInterests",
                column: "UploadId",
                principalTable: "LookupUploads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LookupNeeds_LookupUploads_UploadId",
                table: "LookupNeeds",
                column: "UploadId",
                principalTable: "LookupUploads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
