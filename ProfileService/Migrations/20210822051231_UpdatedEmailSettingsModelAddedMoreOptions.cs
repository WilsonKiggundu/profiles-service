using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileService.Migrations
{
    public partial class UpdatedEmailSettingsModelAddedMoreOptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ApplyForJobReminder",
                table: "EmailPreferences",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ArticleIsPosted",
                table: "EmailPreferences",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "JobAppliedForReminder",
                table: "EmailPreferences",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "YourPostIsLiked",
                table: "EmailPreferences",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplyForJobReminder",
                table: "EmailPreferences");

            migrationBuilder.DropColumn(
                name: "ArticleIsPosted",
                table: "EmailPreferences");

            migrationBuilder.DropColumn(
                name: "JobAppliedForReminder",
                table: "EmailPreferences");

            migrationBuilder.DropColumn(
                name: "YourPostIsLiked",
                table: "EmailPreferences");
        }
    }
}
