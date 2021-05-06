using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileService.Migrations
{
    public partial class AddedFreelanceProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FreelanceProjectId",
                table: "LookupUploads",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FreelanceProjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<string>(nullable: true),
                    DateLastUpdated = table.Column<string>(nullable: true),
                    OwnerId = table.Column<Guid>(nullable: true),
                    OwnerEmail = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Skills = table.Column<string>(nullable: true),
                    PaymentOption = table.Column<string>(nullable: true),
                    Budget = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    HiredPersonId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreelanceProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FreelanceProjects_Persons_HiredPersonId",
                        column: x => x.HiredPersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LookupUploads_FreelanceProjectId",
                table: "LookupUploads",
                column: "FreelanceProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_FreelanceProjects_HiredPersonId",
                table: "FreelanceProjects",
                column: "HiredPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_LookupUploads_FreelanceProjects_FreelanceProjectId",
                table: "LookupUploads",
                column: "FreelanceProjectId",
                principalTable: "FreelanceProjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LookupUploads_FreelanceProjects_FreelanceProjectId",
                table: "LookupUploads");

            migrationBuilder.DropTable(
                name: "FreelanceProjects");

            migrationBuilder.DropIndex(
                name: "IX_LookupUploads_FreelanceProjectId",
                table: "LookupUploads");

            migrationBuilder.DropColumn(
                name: "FreelanceProjectId",
                table: "LookupUploads");
        }
    }
}
