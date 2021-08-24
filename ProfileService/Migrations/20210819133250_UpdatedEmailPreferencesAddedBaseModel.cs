using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileService.Migrations
{
    public partial class UpdatedEmailPreferencesAddedBaseModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailPreferences",
                table: "EmailPreferences");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "EmailPreferences",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "DateCreated",
                table: "EmailPreferences",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateLastUpdated",
                table: "EmailPreferences",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "EmailPreferences",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailPreferences",
                table: "EmailPreferences",
                columns: new[] { "PersonId", "Id" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailPreferences",
                table: "EmailPreferences");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "EmailPreferences");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "EmailPreferences");

            migrationBuilder.DropColumn(
                name: "DateLastUpdated",
                table: "EmailPreferences");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "EmailPreferences");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailPreferences",
                table: "EmailPreferences",
                column: "PersonId");
        }
    }
}
