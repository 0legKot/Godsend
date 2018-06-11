using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Godsend.Migrations
{
    public partial class omg4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AllData_ValueId",
                table: "AllData");

            migrationBuilder.AddColumn<string>(
                name: "ColumnType",
                table: "Column",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Column",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DirId",
                table: "AllData",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AllData_DirId",
                table: "AllData",
                column: "DirId");

            migrationBuilder.CreateIndex(
                name: "IX_AllData_ValueId",
                table: "AllData",
                column: "ValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_AllData_Directory_DirId",
                table: "AllData",
                column: "DirId",
                principalTable: "Directory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllData_Directory_DirId",
                table: "AllData");

            migrationBuilder.DropIndex(
                name: "IX_AllData_DirId",
                table: "AllData");

            migrationBuilder.DropIndex(
                name: "IX_AllData_ValueId",
                table: "AllData");

            migrationBuilder.DropColumn(
                name: "ColumnType",
                table: "Column");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Column");

            migrationBuilder.DropColumn(
                name: "DirId",
                table: "AllData");

            migrationBuilder.CreateIndex(
                name: "IX_AllData_ValueId",
                table: "AllData",
                column: "ValueId",
                unique: true,
                filter: "[ValueId] IS NOT NULL");
        }
    }
}
