using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSoftware.BackgroundJobs.DemoApp.Migrations;

public partial class Initial : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "SmartSoftwareBackgroundJobs",
            columns: table => new {
                Id = table.Column<Guid>(nullable: false),
                ExtraProperties = table.Column<string>(nullable: true),
                ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                JobName = table.Column<string>(maxLength: 128, nullable: false),
                JobArgs = table.Column<string>(maxLength: 1048576, nullable: false),
                TryCount = table.Column<short>(nullable: false, defaultValue: (short)0),
                CreationTime = table.Column<DateTime>(nullable: false),
                NextTryTime = table.Column<DateTime>(nullable: false),
                LastTryTime = table.Column<DateTime>(nullable: true),
                IsAbandoned = table.Column<bool>(nullable: false, defaultValue: false),
                Priority = table.Column<byte>(nullable: false, defaultValue: (byte)15)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_SmartSoftwareBackgroundJobs", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_SmartSoftwareBackgroundJobs_IsAbandoned_NextTryTime",
            table: "SmartSoftwareBackgroundJobs",
            columns: new[] { "IsAbandoned", "NextTryTime" });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "SmartSoftwareBackgroundJobs");
    }
}
