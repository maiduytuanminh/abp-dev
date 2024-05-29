using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSoftware.BloggingTestApp.EntityFrameworkCore.Migrations
{
    public partial class Identity_Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BlgUsers",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "BlgUsers",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "SmartSoftwareUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "SmartSoftwareUsers",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "SmartSoftwareUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "SmartSoftwareUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SmartSoftwareUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "SmartSoftwareUsers",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "SmartSoftwareUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SmartSoftwareUsers",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "SmartSoftwareUsers",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "SmartSoftwareRoles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "SmartSoftwareRoles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsStatic",
                table: "SmartSoftwareRoles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "SmartSoftwareClaimTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Required = table.Column<bool>(nullable: false),
                    IsStatic = table.Column<bool>(nullable: false),
                    Regex = table.Column<string>(maxLength: 512, nullable: true),
                    RegexDescription = table.Column<string>(maxLength: 128, nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    ValueType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartSoftwareClaimTypes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SmartSoftwareClaimTypes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "BlgUsers");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "BlgUsers");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "SmartSoftwareUsers");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "SmartSoftwareUsers");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "SmartSoftwareUsers");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "SmartSoftwareUsers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SmartSoftwareUsers");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "SmartSoftwareUsers");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "SmartSoftwareUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SmartSoftwareUsers");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "SmartSoftwareUsers");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "SmartSoftwareRoles");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "SmartSoftwareRoles");

            migrationBuilder.DropColumn(
                name: "IsStatic",
                table: "SmartSoftwareRoles");
        }
    }
}
