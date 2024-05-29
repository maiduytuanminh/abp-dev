using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartSoftware.BloggingTestApp.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class AddedBlogUserOptionals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Biography",
                table: "BlgUsers",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "BlgUsers",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Github",
                table: "BlgUsers",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "BlgUsers",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Linkedin",
                table: "BlgUsers",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Twitter",
                table: "BlgUsers",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebSite",
                table: "BlgUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EntityVersion",
                table: "SmartSoftwareUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastPasswordChangeTime",
                table: "SmartSoftwareUsers",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ShouldChangePasswordOnNextLogin",
                table: "SmartSoftwareUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "EntityVersion",
                table: "SmartSoftwareRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EntityVersion",
                table: "SmartSoftwareOrganizationUnits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SmartSoftwarePermissionGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartSoftwarePermissionGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmartSoftwarePermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ParentName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    MultiTenancySide = table.Column<byte>(type: "tinyint", nullable: false),
                    Providers = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    StateCheckers = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartSoftwarePermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmartSoftwareUserDelegations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SourceUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TargetUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartSoftwareUserDelegations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SmartSoftwarePermissionGroups_Name",
                table: "SmartSoftwarePermissionGroups",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SmartSoftwarePermissions_GroupName",
                table: "SmartSoftwarePermissions",
                column: "GroupName");

            migrationBuilder.CreateIndex(
                name: "IX_SmartSoftwarePermissions_Name",
                table: "SmartSoftwarePermissions",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SmartSoftwarePermissionGroups");

            migrationBuilder.DropTable(
                name: "SmartSoftwarePermissions");

            migrationBuilder.DropTable(
                name: "SmartSoftwareUserDelegations");

            migrationBuilder.DropColumn(
                name: "Biography",
                table: "BlgUsers");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "BlgUsers");

            migrationBuilder.DropColumn(
                name: "Github",
                table: "BlgUsers");

            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "BlgUsers");

            migrationBuilder.DropColumn(
                name: "Linkedin",
                table: "BlgUsers");

            migrationBuilder.DropColumn(
                name: "Twitter",
                table: "BlgUsers");

            migrationBuilder.DropColumn(
                name: "WebSite",
                table: "BlgUsers");

            migrationBuilder.DropColumn(
                name: "EntityVersion",
                table: "SmartSoftwareUsers");

            migrationBuilder.DropColumn(
                name: "LastPasswordChangeTime",
                table: "SmartSoftwareUsers");

            migrationBuilder.DropColumn(
                name: "ShouldChangePasswordOnNextLogin",
                table: "SmartSoftwareUsers");

            migrationBuilder.DropColumn(
                name: "EntityVersion",
                table: "SmartSoftwareRoles");

            migrationBuilder.DropColumn(
                name: "EntityVersion",
                table: "SmartSoftwareOrganizationUnits");
        }
    }
}
