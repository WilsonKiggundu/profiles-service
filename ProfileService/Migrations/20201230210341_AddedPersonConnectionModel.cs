using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileService.Migrations
{
    public partial class AddedPersonConnectionModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonConnections",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<string>(nullable: true),
                    DateLastUpdated = table.Column<string>(nullable: true),
                    FollowerId = table.Column<Guid>(nullable: false),
                    FolloweeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonConnections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonConnections_Persons_FolloweeId",
                        column: x => x.FolloweeId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonConnections_Persons_FollowerId",
                        column: x => x.FollowerId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonConnections_FolloweeId",
                table: "PersonConnections",
                column: "FolloweeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonConnections_FollowerId",
                table: "PersonConnections",
                column: "FollowerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonConnections");
        }
    }
}
