using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileService.Migrations
{
    public partial class UpdatedPersonConnectionModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonConnections_Persons_FolloweeId",
                table: "PersonConnections");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonConnections_Persons_FollowerId",
                table: "PersonConnections");

            migrationBuilder.DropIndex(
                name: "IX_PersonConnections_FolloweeId",
                table: "PersonConnections");

            migrationBuilder.DropIndex(
                name: "IX_PersonConnections_FollowerId",
                table: "PersonConnections");

            migrationBuilder.DropColumn(
                name: "FolloweeId",
                table: "PersonConnections");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonId",
                table: "PersonConnections",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PersonConnections_PersonId",
                table: "PersonConnections",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonConnections_Persons_PersonId",
                table: "PersonConnections",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonConnections_Persons_PersonId",
                table: "PersonConnections");

            migrationBuilder.DropIndex(
                name: "IX_PersonConnections_PersonId",
                table: "PersonConnections");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "PersonConnections");

            migrationBuilder.AddColumn<Guid>(
                name: "FolloweeId",
                table: "PersonConnections",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PersonConnections_FolloweeId",
                table: "PersonConnections",
                column: "FolloweeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonConnections_FollowerId",
                table: "PersonConnections",
                column: "FollowerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonConnections_Persons_FolloweeId",
                table: "PersonConnections",
                column: "FolloweeId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonConnections_Persons_FollowerId",
                table: "PersonConnections",
                column: "FollowerId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
