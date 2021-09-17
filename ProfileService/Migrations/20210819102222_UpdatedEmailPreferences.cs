using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileService.Migrations
{
    public partial class UpdatedEmailPreferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IfYourProfileIsIncomplete",
                table: "EmailPreferences");

            migrationBuilder.DropColumn(
                name: "WhenACommentIsAddedOnYourPost",
                table: "EmailPreferences");

            migrationBuilder.DropColumn(
                name: "WhenAJobIsPosted",
                table: "EmailPreferences");

            migrationBuilder.DropColumn(
                name: "WhenAnEvenIsPosted",
                table: "EmailPreferences");

            migrationBuilder.DropColumn(
                name: "WhenPeopleYouFollowPostSomething",
                table: "EmailPreferences");

            migrationBuilder.DropColumn(
                name: "WhenTheStartupYouFollowUpdatesProfile",
                table: "EmailPreferences");

            migrationBuilder.DropColumn(
                name: "WhenThereAreSystemUpdates",
                table: "EmailPreferences");

            migrationBuilder.DropColumn(
                name: "WhenYouAreAddedToAStartup",
                table: "EmailPreferences");

            migrationBuilder.DropColumn(
                name: "WhenYouAreFollowed",
                table: "EmailPreferences");

            migrationBuilder.AddColumn<bool>(
                name: "CommentIsAddedOnYourPost",
                table: "EmailPreferences",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EventIsPosted",
                table: "EmailPreferences",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EventReminders",
                table: "EmailPreferences",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "JobIsPosted",
                table: "EmailPreferences",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PeopleYouFollowPostSomething",
                table: "EmailPreferences",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ProfileIsIncomplete",
                table: "EmailPreferences",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TheStartupYouFollowUpdatesProfile",
                table: "EmailPreferences",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ThereAreSystemUpdates",
                table: "EmailPreferences",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WeeklyBlogDigest",
                table: "EmailPreferences",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "YouAreAddedToAStartup",
                table: "EmailPreferences",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "YouAreFollowed",
                table: "EmailPreferences",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentIsAddedOnYourPost",
                table: "EmailPreferences");

            migrationBuilder.DropColumn(
                name: "EventIsPosted",
                table: "EmailPreferences");

            migrationBuilder.DropColumn(
                name: "EventReminders",
                table: "EmailPreferences");

            migrationBuilder.DropColumn(
                name: "JobIsPosted",
                table: "EmailPreferences");

            migrationBuilder.DropColumn(
                name: "PeopleYouFollowPostSomething",
                table: "EmailPreferences");

            migrationBuilder.DropColumn(
                name: "ProfileIsIncomplete",
                table: "EmailPreferences");

            migrationBuilder.DropColumn(
                name: "TheStartupYouFollowUpdatesProfile",
                table: "EmailPreferences");

            migrationBuilder.DropColumn(
                name: "ThereAreSystemUpdates",
                table: "EmailPreferences");

            migrationBuilder.DropColumn(
                name: "WeeklyBlogDigest",
                table: "EmailPreferences");

            migrationBuilder.DropColumn(
                name: "YouAreAddedToAStartup",
                table: "EmailPreferences");

            migrationBuilder.DropColumn(
                name: "YouAreFollowed",
                table: "EmailPreferences");

            migrationBuilder.AddColumn<bool>(
                name: "IfYourProfileIsIncomplete",
                table: "EmailPreferences",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WhenACommentIsAddedOnYourPost",
                table: "EmailPreferences",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WhenAJobIsPosted",
                table: "EmailPreferences",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WhenAnEvenIsPosted",
                table: "EmailPreferences",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WhenPeopleYouFollowPostSomething",
                table: "EmailPreferences",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WhenTheStartupYouFollowUpdatesProfile",
                table: "EmailPreferences",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WhenThereAreSystemUpdates",
                table: "EmailPreferences",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WhenYouAreAddedToAStartup",
                table: "EmailPreferences",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WhenYouAreFollowed",
                table: "EmailPreferences",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
