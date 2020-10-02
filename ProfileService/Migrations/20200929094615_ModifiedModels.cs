using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileService.Migrations
{
    public partial class ModifiedModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessNeeds_Need_NeedId",
                table: "BusinessNeeds");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_Upload_IconId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Interests_Upload_UploadId",
                table: "Interests");

            migrationBuilder.DropForeignKey(
                name: "FK_Need_Upload_UploadId",
                table: "Need");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonCategories_Category_CategoryId",
                table: "PersonCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Upload_UploadId",
                table: "Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Upload",
                table: "Upload");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Need",
                table: "Need");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "Upload",
                newName: "Uploads");

            migrationBuilder.RenameTable(
                name: "Need",
                newName: "Needs");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_Need_UploadId",
                table: "Needs",
                newName: "IX_Needs_UploadId");

            migrationBuilder.RenameIndex(
                name: "IX_Category_IconId",
                table: "Categories",
                newName: "IX_Categories_IconId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Uploads",
                table: "Uploads",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Needs",
                table: "Needs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessNeeds_Needs_NeedId",
                table: "BusinessNeeds",
                column: "NeedId",
                principalTable: "Needs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Uploads_IconId",
                table: "Categories",
                column: "IconId",
                principalTable: "Uploads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Interests_Uploads_UploadId",
                table: "Interests",
                column: "UploadId",
                principalTable: "Uploads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Needs_Uploads_UploadId",
                table: "Needs",
                column: "UploadId",
                principalTable: "Uploads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonCategories_Categories_CategoryId",
                table: "PersonCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Uploads_UploadId",
                table: "Persons",
                column: "UploadId",
                principalTable: "Uploads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessNeeds_Needs_NeedId",
                table: "BusinessNeeds");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Uploads_IconId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Interests_Uploads_UploadId",
                table: "Interests");

            migrationBuilder.DropForeignKey(
                name: "FK_Needs_Uploads_UploadId",
                table: "Needs");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonCategories_Categories_CategoryId",
                table: "PersonCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Uploads_UploadId",
                table: "Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Uploads",
                table: "Uploads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Needs",
                table: "Needs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Uploads",
                newName: "Upload");

            migrationBuilder.RenameTable(
                name: "Needs",
                newName: "Need");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_Needs_UploadId",
                table: "Need",
                newName: "IX_Need_UploadId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_IconId",
                table: "Category",
                newName: "IX_Category_IconId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Upload",
                table: "Upload",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Need",
                table: "Need",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessNeeds_Need_NeedId",
                table: "BusinessNeeds",
                column: "NeedId",
                principalTable: "Need",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Upload_IconId",
                table: "Category",
                column: "IconId",
                principalTable: "Upload",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Interests_Upload_UploadId",
                table: "Interests",
                column: "UploadId",
                principalTable: "Upload",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Need_Upload_UploadId",
                table: "Need",
                column: "UploadId",
                principalTable: "Upload",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonCategories_Category_CategoryId",
                table: "PersonCategories",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Upload_UploadId",
                table: "Persons",
                column: "UploadId",
                principalTable: "Upload",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
