using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileService.Migrations
{
    public partial class UpdatedDevicesModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOnline",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "LastSeen",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "PushAuth",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "PushEndpoint",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "PushP256DH",
                table: "Devices");

            migrationBuilder.AddColumn<string>(
                name: "ProfileId",
                table: "Devices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Devices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Devices");

            migrationBuilder.AddColumn<bool>(
                name: "IsOnline",
                table: "Devices",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastSeen",
                table: "Devices",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Devices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PushAuth",
                table: "Devices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PushEndpoint",
                table: "Devices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PushP256DH",
                table: "Devices",
                type: "text",
                nullable: true);
        }
    }
}
