using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileService.Migrations
{
    public partial class AddedContactModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateLastUpdated = table.Column<DateTime>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: false),
                    BelongsTo = table.Column<Guid>(nullable: false),
                    Details = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}
