using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSoftware.BloggingTestApp.EntityFrameworkCore.Migrations
{
    public partial class Added_OrganizationUnits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SmartSoftwareOrganizationUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    ParentId = table.Column<Guid>(nullable: true),
                    Code = table.Column<string>(maxLength: 95, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartSoftwareOrganizationUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmartSoftwareOrganizationUnits_SmartSoftwareOrganizationUnits_ParentId",
                        column: x => x.ParentId,
                        principalTable: "SmartSoftwareOrganizationUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SmartSoftwareOrganizationUnitRoles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(nullable: false),
                    OrganizationUnitId = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartSoftwareOrganizationUnitRoles", x => new { x.OrganizationUnitId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_SmartSoftwareOrganizationUnitRoles_SmartSoftwareOrganizationUnits_OrganizationUnitId",
                        column: x => x.OrganizationUnitId,
                        principalTable: "SmartSoftwareOrganizationUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SmartSoftwareOrganizationUnitRoles_SmartSoftwareRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "SmartSoftwareRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SmartSoftwareUserOrganizationUnits",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    OrganizationUnitId = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartSoftwareUserOrganizationUnits", x => new { x.OrganizationUnitId, x.UserId });
                    table.ForeignKey(
                        name: "FK_SmartSoftwareUserOrganizationUnits_SmartSoftwareOrganizationUnits_OrganizationUnitId",
                        column: x => x.OrganizationUnitId,
                        principalTable: "SmartSoftwareOrganizationUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SmartSoftwareUserOrganizationUnits_SmartSoftwareUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "SmartSoftwareUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SmartSoftwareOrganizationUnitRoles_RoleId_OrganizationUnitId",
                table: "SmartSoftwareOrganizationUnitRoles",
                columns: new[] { "RoleId", "OrganizationUnitId" });

            migrationBuilder.CreateIndex(
                name: "IX_SmartSoftwareOrganizationUnits_Code",
                table: "SmartSoftwareOrganizationUnits",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_SmartSoftwareOrganizationUnits_ParentId",
                table: "SmartSoftwareOrganizationUnits",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartSoftwareUserOrganizationUnits_UserId_OrganizationUnitId",
                table: "SmartSoftwareUserOrganizationUnits",
                columns: new[] { "UserId", "OrganizationUnitId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SmartSoftwareOrganizationUnitRoles");

            migrationBuilder.DropTable(
                name: "SmartSoftwareUserOrganizationUnits");

            migrationBuilder.DropTable(
                name: "SmartSoftwareOrganizationUnits");
        }
    }
}
