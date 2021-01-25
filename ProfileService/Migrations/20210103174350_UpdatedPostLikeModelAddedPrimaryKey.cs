using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileService.Migrations
{
    public partial class UpdatedPostLikeModelAddedPrimaryKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostLike_Persons_PersonId",
                table: "PostLike");

            migrationBuilder.DropForeignKey(
                name: "FK_PostLike_Posts_PostId",
                table: "PostLike");

            migrationBuilder.DropIndex(
                name: "IX_PostLike_PostId_PersonId",
                table: "PostLike");

            migrationBuilder.RenameTable(
                name: "PostLike",
                newName: "PostLikes");

            migrationBuilder.RenameIndex(
                name: "IX_PostLike_PersonId",
                table: "PostLikes",
                newName: "IX_PostLikes_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostLikes",
                table: "PostLikes",
                columns: new[] { "PostId", "PersonId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PostLikes_Persons_PersonId",
                table: "PostLikes",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostLikes_Posts_PostId",
                table: "PostLikes",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostLikes_Persons_PersonId",
                table: "PostLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_PostLikes_Posts_PostId",
                table: "PostLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostLikes",
                table: "PostLikes");

            migrationBuilder.RenameTable(
                name: "PostLikes",
                newName: "PostLike");

            migrationBuilder.RenameIndex(
                name: "IX_PostLikes_PersonId",
                table: "PostLike",
                newName: "IX_PostLike_PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PostLike_PostId_PersonId",
                table: "PostLike",
                columns: new[] { "PostId", "PersonId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PostLike_Persons_PersonId",
                table: "PostLike",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostLike_Posts_PostId",
                table: "PostLike",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
