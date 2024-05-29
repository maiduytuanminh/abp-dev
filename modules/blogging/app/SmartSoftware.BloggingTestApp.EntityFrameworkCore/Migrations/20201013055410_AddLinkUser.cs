﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSoftware.BloggingTestApp.EntityFrameworkCore.Migrations
{
    public partial class AddLinkUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SmartSoftwareLinkUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SourceUserId = table.Column<Guid>(nullable: false),
                    SourceTenantId = table.Column<Guid>(nullable: true),
                    TargetUserId = table.Column<Guid>(nullable: false),
                    TargetTenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartSoftwareLinkUsers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SmartSoftwareLinkUsers_SourceUserId_SourceTenantId_TargetUserId_TargetTenantId",
                table: "SmartSoftwareLinkUsers",
                columns: new[] { "SourceUserId", "SourceTenantId", "TargetUserId", "TargetTenantId" },
                unique: true,
                filter: "[SourceTenantId] IS NOT NULL AND [TargetTenantId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SmartSoftwareLinkUsers");
        }
    }
}
