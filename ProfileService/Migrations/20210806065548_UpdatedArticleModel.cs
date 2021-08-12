using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileService.Migrations
{
    public partial class UpdatedArticleModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Persons_AuthorId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_AuthorId",
                table: "Articles");

            migrationBuilder.AddColumn<Guid>(
                name: "ArticleId",
                table: "LookupUploads",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ArticleId",
                table: "Likes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "Articles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LookupUploads_ArticleId",
                table: "LookupUploads",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_ArticleId",
                table: "Likes",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Articles_ArticleId",
                table: "Likes",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LookupUploads_Articles_ArticleId",
                table: "LookupUploads",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Articles_ArticleId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_LookupUploads_Articles_ArticleId",
                table: "LookupUploads");

            migrationBuilder.DropIndex(
                name: "IX_LookupUploads_ArticleId",
                table: "LookupUploads");

            migrationBuilder.DropIndex(
                name: "IX_Likes_ArticleId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "LookupUploads");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "Articles");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_AuthorId",
                table: "Articles",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Persons_AuthorId",
                table: "Articles",
                column: "AuthorId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
