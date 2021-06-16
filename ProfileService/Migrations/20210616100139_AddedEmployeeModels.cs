using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProfileService.Migrations
{
    public partial class AddedEmployeeModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "employees");

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<string>(nullable: true),
                    DateLastUpdated = table.Column<string>(nullable: true),
                    BusinessId = table.Column<Guid>(nullable: false),
                    Department = table.Column<int>(nullable: true),
                    Firstname = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                schema: "employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<string>(nullable: true),
                    DateLastUpdated = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    WorkStation = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "employees",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wellness",
                schema: "employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<string>(nullable: true),
                    DateLastUpdated = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wellness", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wellness_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "employees",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_EmployeeId",
                schema: "employees",
                table: "Schedules",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Wellness_EmployeeId",
                schema: "employees",
                table: "Wellness",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schedules",
                schema: "employees");

            migrationBuilder.DropTable(
                name: "Wellness",
                schema: "employees");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "employees");
        }
    }
}
