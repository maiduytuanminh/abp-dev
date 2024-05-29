using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSoftware.BloggingTestApp.EntityFrameworkCore.Migrations
{
    public partial class Added_BlobStoing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SmartSoftwareBlobContainers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartSoftwareBlobContainers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmartSoftwareBlobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    ContainerId = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Content = table.Column<byte[]>(maxLength: 2147483647, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartSoftwareBlobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmartSoftwareBlobs_SmartSoftwareBlobContainers_ContainerId",
                        column: x => x.ContainerId,
                        principalTable: "SmartSoftwareBlobContainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SmartSoftwareBlobContainers_TenantId_Name",
                table: "SmartSoftwareBlobContainers",
                columns: new[] { "TenantId", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_SmartSoftwareBlobs_ContainerId",
                table: "SmartSoftwareBlobs",
                column: "ContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartSoftwareBlobs_TenantId_ContainerId_Name",
                table: "SmartSoftwareBlobs",
                columns: new[] { "TenantId", "ContainerId", "Name" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SmartSoftwareBlobs");

            migrationBuilder.DropTable(
                name: "SmartSoftwareBlobContainers");
        }
    }
}
