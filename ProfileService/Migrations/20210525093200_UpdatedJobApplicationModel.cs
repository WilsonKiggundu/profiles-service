using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileService.Migrations
{
    public partial class UpdatedJobApplicationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "JobId",
                "JobApplications");

            migrationBuilder.AddColumn<Guid>(
                "JobId",
                "JobApplications",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobId",
                table: "JobApplications");
            
            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "JobApplications",
                type: "integer",
                nullable: false);
        }
    }
}
