using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSoftware.BloggingTestApp.EntityFrameworkCore.Migrations
{
    public partial class Added_Identity_Module : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SmartSoftwarePermissionGrants",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderName = table.Column<string>(maxLength: 64, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartSoftwarePermissionGrants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmartSoftwareRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartSoftwareRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmartSoftwareSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(maxLength: 2048, nullable: false),
                    ProviderName = table.Column<string>(maxLength: 64, nullable: true),
                    ProviderKey = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartSoftwareSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmartSoftwareUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true),
                    UserName = table.Column<string>(maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false, defaultValue: false),
                    PasswordHash = table.Column<string>(maxLength: 256, nullable: true),
                    SecurityStamp = table.Column<string>(maxLength: 256, nullable: false),
                    ConcurrencyStamp = table.Column<string>(maxLength: 256, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 16, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false, defaultValue: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false, defaultValue: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false, defaultValue: false),
                    AccessFailedCount = table.Column<int>(nullable: false, defaultValue: 0),
                    ExtraProperties = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartSoftwareUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmartSoftwareRoleClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true),
                    ClaimType = table.Column<string>(maxLength: 256, nullable: false),
                    ClaimValue = table.Column<string>(maxLength: 1024, nullable: true),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartSoftwareRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmartSoftwareRoleClaims_SmartSoftwareRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "SmartSoftwareRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SmartSoftwareUserClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: true),
                    ClaimType = table.Column<string>(maxLength: 256, nullable: false),
                    ClaimValue = table.Column<string>(maxLength: 1024, nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartSoftwareUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmartSoftwareUserClaims_SmartSoftwareUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "SmartSoftwareUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SmartSoftwareUserLogins",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 64, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 196, nullable: false),
                    ProviderDisplayName = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartSoftwareUserLogins", x => new { x.UserId, x.LoginProvider });
                    table.ForeignKey(
                        name: "FK_SmartSoftwareUserLogins_SmartSoftwareUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "SmartSoftwareUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SmartSoftwareUserRoles",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartSoftwareUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_SmartSoftwareUserRoles_SmartSoftwareRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "SmartSoftwareRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SmartSoftwareUserRoles_SmartSoftwareUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "SmartSoftwareUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SmartSoftwareUserTokens",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartSoftwareUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_SmartSoftwareUserTokens_SmartSoftwareUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "SmartSoftwareUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SmartSoftwarePermissionGrants_Name_ProviderName_ProviderKey",
                table: "SmartSoftwarePermissionGrants",
                columns: new[] { "Name", "ProviderName", "ProviderKey" });

            migrationBuilder.CreateIndex(
                name: "IX_SmartSoftwareRoleClaims_RoleId",
                table: "SmartSoftwareRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartSoftwareRoles_NormalizedName",
                table: "SmartSoftwareRoles",
                column: "NormalizedName");

            migrationBuilder.CreateIndex(
                name: "IX_SmartSoftwareSettings_Name_ProviderName_ProviderKey",
                table: "SmartSoftwareSettings",
                columns: new[] { "Name", "ProviderName", "ProviderKey" });

            migrationBuilder.CreateIndex(
                name: "IX_SmartSoftwareUserClaims_UserId",
                table: "SmartSoftwareUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartSoftwareUserLogins_LoginProvider_ProviderKey",
                table: "SmartSoftwareUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.CreateIndex(
                name: "IX_SmartSoftwareUserRoles_RoleId_UserId",
                table: "SmartSoftwareUserRoles",
                columns: new[] { "RoleId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_SmartSoftwareUsers_Email",
                table: "SmartSoftwareUsers",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_SmartSoftwareUsers_NormalizedEmail",
                table: "SmartSoftwareUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_SmartSoftwareUsers_NormalizedUserName",
                table: "SmartSoftwareUsers",
                column: "NormalizedUserName");

            migrationBuilder.CreateIndex(
                name: "IX_SmartSoftwareUsers_UserName",
                table: "SmartSoftwareUsers",
                column: "UserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SmartSoftwarePermissionGrants");

            migrationBuilder.DropTable(
                name: "SmartSoftwareRoleClaims");

            migrationBuilder.DropTable(
                name: "SmartSoftwareSettings");

            migrationBuilder.DropTable(
                name: "SmartSoftwareUserClaims");

            migrationBuilder.DropTable(
                name: "SmartSoftwareUserLogins");

            migrationBuilder.DropTable(
                name: "SmartSoftwareUserRoles");

            migrationBuilder.DropTable(
                name: "SmartSoftwareUserTokens");

            migrationBuilder.DropTable(
                name: "SmartSoftwareRoles");

            migrationBuilder.DropTable(
                name: "SmartSoftwareUsers");
        }
    }
}
