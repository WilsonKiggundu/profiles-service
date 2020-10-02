using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileService.Migrations
{
    public partial class ModifiedLookupModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessInterests_Interests_InterestId",
                table: "BusinessInterests");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessNeeds_Needs_NeedId",
                table: "BusinessNeeds");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Uploads_IconId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Interests_Persons_PersonId",
                table: "Interests");

            migrationBuilder.DropForeignKey(
                name: "FK_Interests_Uploads_UploadId",
                table: "Interests");

            migrationBuilder.DropForeignKey(
                name: "FK_InvestorInterests_Interests_InterestId",
                table: "InvestorInterests");

            migrationBuilder.DropForeignKey(
                name: "FK_Needs_Uploads_UploadId",
                table: "Needs");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonCategories_Categories_CategoryId",
                table: "PersonCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonInterests_Interests_InterestId",
                table: "PersonInterests");

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
                name: "PK_Interests",
                table: "Interests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Uploads",
                newName: "LookupUploads");

            migrationBuilder.RenameTable(
                name: "Needs",
                newName: "LookupNeeds");

            migrationBuilder.RenameTable(
                name: "Interests",
                newName: "LookupInterests");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "LookupCategories");

            migrationBuilder.RenameIndex(
                name: "IX_Needs_UploadId",
                table: "LookupNeeds",
                newName: "IX_LookupNeeds_UploadId");

            migrationBuilder.RenameIndex(
                name: "IX_Interests_UploadId",
                table: "LookupInterests",
                newName: "IX_LookupInterests_UploadId");

            migrationBuilder.RenameIndex(
                name: "IX_Interests_PersonId",
                table: "LookupInterests",
                newName: "IX_LookupInterests_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_IconId",
                table: "LookupCategories",
                newName: "IX_LookupCategories_IconId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LookupUploads",
                table: "LookupUploads",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LookupNeeds",
                table: "LookupNeeds",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LookupInterests",
                table: "LookupInterests",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LookupCategories",
                table: "LookupCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessInterests_LookupInterests_InterestId",
                table: "BusinessInterests",
                column: "InterestId",
                principalTable: "LookupInterests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessNeeds_LookupNeeds_NeedId",
                table: "BusinessNeeds",
                column: "NeedId",
                principalTable: "LookupNeeds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvestorInterests_LookupInterests_InterestId",
                table: "InvestorInterests",
                column: "InterestId",
                principalTable: "LookupInterests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LookupCategories_LookupUploads_IconId",
                table: "LookupCategories",
                column: "IconId",
                principalTable: "LookupUploads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LookupInterests_Persons_PersonId",
                table: "LookupInterests",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

            migrationBuilder.AddForeignKey(
                name: "FK_PersonCategories_LookupCategories_CategoryId",
                table: "PersonCategories",
                column: "CategoryId",
                principalTable: "LookupCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonInterests_LookupInterests_InterestId",
                table: "PersonInterests",
                column: "InterestId",
                principalTable: "LookupInterests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_LookupUploads_UploadId",
                table: "Persons",
                column: "UploadId",
                principalTable: "LookupUploads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessInterests_LookupInterests_InterestId",
                table: "BusinessInterests");

            migrationBuilder.DropForeignKey(
                name: "FK_BusinessNeeds_LookupNeeds_NeedId",
                table: "BusinessNeeds");

            migrationBuilder.DropForeignKey(
                name: "FK_InvestorInterests_LookupInterests_InterestId",
                table: "InvestorInterests");

            migrationBuilder.DropForeignKey(
                name: "FK_LookupCategories_LookupUploads_IconId",
                table: "LookupCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_LookupInterests_Persons_PersonId",
                table: "LookupInterests");

            migrationBuilder.DropForeignKey(
                name: "FK_LookupInterests_LookupUploads_UploadId",
                table: "LookupInterests");

            migrationBuilder.DropForeignKey(
                name: "FK_LookupNeeds_LookupUploads_UploadId",
                table: "LookupNeeds");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonCategories_LookupCategories_CategoryId",
                table: "PersonCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonInterests_LookupInterests_InterestId",
                table: "PersonInterests");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_LookupUploads_UploadId",
                table: "Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LookupUploads",
                table: "LookupUploads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LookupNeeds",
                table: "LookupNeeds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LookupInterests",
                table: "LookupInterests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LookupCategories",
                table: "LookupCategories");

            migrationBuilder.RenameTable(
                name: "LookupUploads",
                newName: "Uploads");

            migrationBuilder.RenameTable(
                name: "LookupNeeds",
                newName: "Needs");

            migrationBuilder.RenameTable(
                name: "LookupInterests",
                newName: "Interests");

            migrationBuilder.RenameTable(
                name: "LookupCategories",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_LookupNeeds_UploadId",
                table: "Needs",
                newName: "IX_Needs_UploadId");

            migrationBuilder.RenameIndex(
                name: "IX_LookupInterests_UploadId",
                table: "Interests",
                newName: "IX_Interests_UploadId");

            migrationBuilder.RenameIndex(
                name: "IX_LookupInterests_PersonId",
                table: "Interests",
                newName: "IX_Interests_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_LookupCategories_IconId",
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
                name: "PK_Interests",
                table: "Interests",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessInterests_Interests_InterestId",
                table: "BusinessInterests",
                column: "InterestId",
                principalTable: "Interests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Interests_Persons_PersonId",
                table: "Interests",
                column: "PersonId",
                principalTable: "Persons",
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
                name: "FK_InvestorInterests_Interests_InterestId",
                table: "InvestorInterests",
                column: "InterestId",
                principalTable: "Interests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_PersonInterests_Interests_InterestId",
                table: "PersonInterests",
                column: "InterestId",
                principalTable: "Interests",
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
    }
}
