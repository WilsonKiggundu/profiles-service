using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileService.Migrations
{
    public partial class UpdatedAddressModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "District",
                table: "InvestorAddresses");

            migrationBuilder.DropColumn(
                name: "Postal",
                table: "InvestorAddresses");

            migrationBuilder.DropColumn(
                name: "District",
                table: "BusinessAddresses");

            migrationBuilder.DropColumn(
                name: "Postal",
                table: "BusinessAddresses");

            migrationBuilder.AddColumn<string>(
                name: "AddressLine",
                table: "InvestorAddresses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "InvestorAddresses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "InvestorAddresses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine",
                table: "BusinessAddresses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "BusinessAddresses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "BusinessAddresses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressLine",
                table: "InvestorAddresses");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "InvestorAddresses");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "InvestorAddresses");

            migrationBuilder.DropColumn(
                name: "AddressLine",
                table: "BusinessAddresses");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "BusinessAddresses");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "BusinessAddresses");

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "InvestorAddresses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Postal",
                table: "InvestorAddresses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "BusinessAddresses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Postal",
                table: "BusinessAddresses",
                type: "text",
                nullable: true);
        }
    }
}
