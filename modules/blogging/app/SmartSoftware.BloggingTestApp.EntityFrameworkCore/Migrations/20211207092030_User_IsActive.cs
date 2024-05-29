using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartSoftware.BloggingTestApp.EntityFrameworkCore.Migrations
{
    public partial class User_IsActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SmartSoftwareSettings_Name_ProviderName_ProviderKey",
                table: "SmartSoftwareSettings");

            migrationBuilder.DropIndex(
                name: "IX_SmartSoftwarePermissionGrants_Name_ProviderName_ProviderKey",
                table: "SmartSoftwarePermissionGrants");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "SmartSoftwareUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_SmartSoftwareSettings_Name_ProviderName_ProviderKey",
                table: "SmartSoftwareSettings",
                columns: new[] { "Name", "ProviderName", "ProviderKey" },
                unique: true,
                filter: "[ProviderName] IS NOT NULL AND [ProviderKey] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SmartSoftwarePermissionGrants_TenantId_Name_ProviderName_ProviderKey",
                table: "SmartSoftwarePermissionGrants",
                columns: new[] { "TenantId", "Name", "ProviderName", "ProviderKey" },
                unique: true,
                filter: "[TenantId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SmartSoftwareSettings_Name_ProviderName_ProviderKey",
                table: "SmartSoftwareSettings");

            migrationBuilder.DropIndex(
                name: "IX_SmartSoftwarePermissionGrants_TenantId_Name_ProviderName_ProviderKey",
                table: "SmartSoftwarePermissionGrants");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "SmartSoftwareUsers");

            migrationBuilder.CreateIndex(
                name: "IX_SmartSoftwareSettings_Name_ProviderName_ProviderKey",
                table: "SmartSoftwareSettings",
                columns: new[] { "Name", "ProviderName", "ProviderKey" });

            migrationBuilder.CreateIndex(
                name: "IX_SmartSoftwarePermissionGrants_Name_ProviderName_ProviderKey",
                table: "SmartSoftwarePermissionGrants",
                columns: new[] { "Name", "ProviderName", "ProviderKey" });
        }
    }
}
