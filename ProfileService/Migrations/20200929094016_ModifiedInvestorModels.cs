using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileService.Migrations
{
    public partial class ModifiedInvestorModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Investors_InvestorId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Businesses_BusinessId",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Upload_BusinessProducts_BusinessProductId",
                table: "Upload");

            migrationBuilder.DropIndex(
                name: "IX_Upload_BusinessProductId",
                table: "Upload");

            migrationBuilder.DropIndex(
                name: "IX_Persons_BusinessId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_InvestorId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "BusinessProductId",
                table: "Upload");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "InvestorId",
                table: "Contacts");

            migrationBuilder.CreateTable(
                name: "BusinessContacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateLastUpdated = table.Column<DateTime>(nullable: true),
                    BusinessId = table.Column<Guid>(nullable: false),
                    ContactId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessContacts_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessContacts_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvestorContacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateLastUpdated = table.Column<DateTime>(nullable: true),
                    InvestorId = table.Column<Guid>(nullable: false),
                    ContactId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestorContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestorContacts_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvestorContacts_Investors_InvestorId",
                        column: x => x.InvestorId,
                        principalTable: "Investors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContacts_BusinessId",
                table: "BusinessContacts",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessContacts_ContactId",
                table: "BusinessContacts",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestorContacts_ContactId",
                table: "InvestorContacts",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestorContacts_InvestorId",
                table: "InvestorContacts",
                column: "InvestorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessContacts");

            migrationBuilder.DropTable(
                name: "InvestorContacts");

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessProductId",
                table: "Upload",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessId",
                table: "Persons",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InvestorId",
                table: "Contacts",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Upload_BusinessProductId",
                table: "Upload",
                column: "BusinessProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_BusinessId",
                table: "Persons",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_InvestorId",
                table: "Contacts",
                column: "InvestorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Investors_InvestorId",
                table: "Contacts",
                column: "InvestorId",
                principalTable: "Investors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Businesses_BusinessId",
                table: "Persons",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Upload_BusinessProducts_BusinessProductId",
                table: "Upload",
                column: "BusinessProductId",
                principalTable: "BusinessProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
