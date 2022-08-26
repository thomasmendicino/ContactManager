﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactManager.Migrations
{
    public partial class Add_Unique_Constraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VendorCode",
                table: "VendorMasterList",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "VendorMasterList",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_VendorMasterList_CompanyName",
                table: "VendorMasterList",
                column: "CompanyName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VendorMasterList_VendorCode",
                table: "VendorMasterList",
                column: "VendorCode",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VendorMasterList_CompanyName",
                table: "VendorMasterList");

            migrationBuilder.DropIndex(
                name: "IX_VendorMasterList_VendorCode",
                table: "VendorMasterList");

            migrationBuilder.AlterColumn<string>(
                name: "VendorCode",
                table: "VendorMasterList",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "VendorMasterList",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
