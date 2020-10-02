using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileService.Migrations
{
    public partial class AddedInvestorModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "PersonCategories");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Interests");

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessId",
                table: "Persons",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UploadId",
                table: "Persons",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "PersonCategories",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UploadId",
                table: "Interests",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Contacts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "InvestorId",
                table: "Contacts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Businesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateLastUpdated = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    EmployeeCount = table.Column<int>(nullable: true),
                    IncorporationDate = table.Column<DateTime>(nullable: true),
                    Website = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Businesses", x => x.Id);
                    table.UniqueConstraint("AlternateKey_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Investors",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateLastUpdated = table.Column<DateTime>(nullable: true),
                    PersonId = table.Column<Guid>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    InvestmentRange = table.Column<string>(nullable: true),
                    InvestmentStage = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true),
                    IsVerified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Investors_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessAddresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateLastUpdated = table.Column<DateTime>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Country = table.Column<string>(nullable: true),
                    District = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Building = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    Floor = table.Column<string>(nullable: true),
                    Postal = table.Column<string>(nullable: true),
                    BusinessId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessAddresses_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessInterests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateLastUpdated = table.Column<DateTime>(nullable: true),
                    BusinessId = table.Column<Guid>(nullable: false),
                    InterestId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessInterests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessInterests_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessInterests_Interests_InterestId",
                        column: x => x.InterestId,
                        principalTable: "Interests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateLastUpdated = table.Column<DateTime>(nullable: true),
                    BusinessId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(maxLength: 280, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessProducts_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusinessRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateLastUpdated = table.Column<DateTime>(nullable: true),
                    BusinessId = table.Column<Guid>(nullable: false),
                    PersonId = table.Column<Guid>(nullable: false),
                    Role = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessRoles_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessRoles_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvestorAddresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateLastUpdated = table.Column<DateTime>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Country = table.Column<string>(nullable: true),
                    District = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Building = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    Floor = table.Column<string>(nullable: true),
                    Postal = table.Column<string>(nullable: true),
                    InvestorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestorAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestorAddresses_Investors_InvestorId",
                        column: x => x.InvestorId,
                        principalTable: "Investors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvestorInterests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateLastUpdated = table.Column<DateTime>(nullable: true),
                    InvestorId = table.Column<Guid>(nullable: false),
                    InterestId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestorInterests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestorInterests_Interests_InterestId",
                        column: x => x.InterestId,
                        principalTable: "Interests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvestorInterests_Investors_InvestorId",
                        column: x => x.InvestorId,
                        principalTable: "Investors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvestorPortfolios",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateLastUpdated = table.Column<DateTime>(nullable: true),
                    InvestorId = table.Column<Guid>(nullable: false),
                    Details = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestorPortfolios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestorPortfolios_Investors_InvestorId",
                        column: x => x.InvestorId,
                        principalTable: "Investors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Upload",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateLastUpdated = table.Column<DateTime>(nullable: true),
                    OwnerId = table.Column<Guid>(nullable: false),
                    FileName = table.Column<string>(nullable: false),
                    FileSize = table.Column<int>(nullable: false),
                    Path = table.Column<string>(nullable: false),
                    ContentType = table.Column<string>(nullable: false),
                    BusinessProductId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Upload", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Upload_BusinessProducts_BusinessProductId",
                        column: x => x.BusinessProductId,
                        principalTable: "BusinessProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateLastUpdated = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    IconId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Upload_IconId",
                        column: x => x.IconId,
                        principalTable: "Upload",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Need",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateLastUpdated = table.Column<DateTime>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    UploadId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Need", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Need_Upload_UploadId",
                        column: x => x.UploadId,
                        principalTable: "Upload",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BusinessNeeds",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateLastUpdated = table.Column<DateTime>(nullable: true),
                    BusinessId = table.Column<Guid>(nullable: false),
                    NeedId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessNeeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessNeeds_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessNeeds_Need_NeedId",
                        column: x => x.NeedId,
                        principalTable: "Need",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_BusinessId",
                table: "Persons",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_UploadId",
                table: "Persons",
                column: "UploadId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonCategories_CategoryId",
                table: "PersonCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Interests_UploadId",
                table: "Interests",
                column: "UploadId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_InvestorId",
                table: "Contacts",
                column: "InvestorId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessAddresses_BusinessId",
                table: "BusinessAddresses",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessInterests_BusinessId",
                table: "BusinessInterests",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessInterests_InterestId",
                table: "BusinessInterests",
                column: "InterestId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessNeeds_BusinessId",
                table: "BusinessNeeds",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessNeeds_NeedId",
                table: "BusinessNeeds",
                column: "NeedId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessProducts_BusinessId",
                table: "BusinessProducts",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessRoles_BusinessId",
                table: "BusinessRoles",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessRoles_PersonId",
                table: "BusinessRoles",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_IconId",
                table: "Category",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestorAddresses_InvestorId",
                table: "InvestorAddresses",
                column: "InvestorId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestorInterests_InterestId",
                table: "InvestorInterests",
                column: "InterestId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestorInterests_InvestorId",
                table: "InvestorInterests",
                column: "InvestorId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestorPortfolios_InvestorId",
                table: "InvestorPortfolios",
                column: "InvestorId");

            migrationBuilder.CreateIndex(
                name: "IX_Investors_PersonId",
                table: "Investors",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Need_UploadId",
                table: "Need",
                column: "UploadId");

            migrationBuilder.CreateIndex(
                name: "IX_Upload_BusinessProductId",
                table: "Upload",
                column: "BusinessProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Investors_InvestorId",
                table: "Contacts",
                column: "InvestorId",
                principalTable: "Investors",
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
                name: "FK_PersonCategories_Category_CategoryId",
                table: "PersonCategories",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Businesses_BusinessId",
                table: "Persons",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Upload_UploadId",
                table: "Persons",
                column: "UploadId",
                principalTable: "Upload",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Investors_InvestorId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Interests_Upload_UploadId",
                table: "Interests");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonCategories_Category_CategoryId",
                table: "PersonCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Businesses_BusinessId",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Upload_UploadId",
                table: "Persons");

            migrationBuilder.DropTable(
                name: "BusinessAddresses");

            migrationBuilder.DropTable(
                name: "BusinessInterests");

            migrationBuilder.DropTable(
                name: "BusinessNeeds");

            migrationBuilder.DropTable(
                name: "BusinessRoles");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "InvestorAddresses");

            migrationBuilder.DropTable(
                name: "InvestorInterests");

            migrationBuilder.DropTable(
                name: "InvestorPortfolios");

            migrationBuilder.DropTable(
                name: "Need");

            migrationBuilder.DropTable(
                name: "Investors");

            migrationBuilder.DropTable(
                name: "Upload");

            migrationBuilder.DropTable(
                name: "BusinessProducts");

            migrationBuilder.DropTable(
                name: "Businesses");

            migrationBuilder.DropIndex(
                name: "IX_Persons_BusinessId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_UploadId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_PersonCategories_CategoryId",
                table: "PersonCategories");

            migrationBuilder.DropIndex(
                name: "IX_Interests_UploadId",
                table: "Interests");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_InvestorId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "UploadId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "PersonCategories");

            migrationBuilder.DropColumn(
                name: "UploadId",
                table: "Interests");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "InvestorId",
                table: "Contacts");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Persons",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "PersonCategories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Interests",
                type: "text",
                nullable: true);
        }
    }
}
