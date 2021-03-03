using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using ProfileService.Models.Preferences;
using ProfileService.Repositories;

namespace ProfileService.Migrations
{
    public partial class AddedEmailSettingsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailPreferences",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(nullable: false),
                    IfYourProfileIsIncomplete = table.Column<bool>(nullable: false),
                    WhenACommentIsAddedOnYourPost = table.Column<bool>(nullable: false),
                    WhenYouAreFollowed = table.Column<bool>(nullable: false),
                    WhenYouAreAddedToAStartup = table.Column<bool>(nullable: false),
                    WhenThereAreSystemUpdates = table.Column<bool>(nullable: false),
                    WhenPeopleYouFollowPostSomething = table.Column<bool>(nullable: false),
                    WhenTheStartupYouFollowUpdatesProfile = table.Column<bool>(nullable: false),
                    WhenAnEvenIsPosted = table.Column<bool>(nullable: false),
                    WhenAJobIsPosted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailPreferences", x => x.PersonId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailPreferences");
        }
    }
}
