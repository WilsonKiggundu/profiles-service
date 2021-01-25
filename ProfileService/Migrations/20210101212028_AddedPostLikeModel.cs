using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileService.Migrations
{
    public partial class AddedPostLikeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostLike",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(nullable: false),
                    PostId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_PostLike_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostLike_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostLike_PersonId",
                table: "PostLike",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PostLike_PostId_PersonId",
                table: "PostLike",
                columns: new[] { "PostId", "PersonId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostLike");
        }
    }
}
