using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Godsend.Migrations
{
    public partial class reworkPaths : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StringWrapper_Products_ProductId",
                table: "StringWrapper");

            migrationBuilder.DropColumn(
                name: "ImagePreviewName",
                table: "Information");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "StringWrapper",
                newName: "ImagePathsId");

            migrationBuilder.RenameIndex(
                name: "IX_StringWrapper_ProductId",
                table: "StringWrapper",
                newName: "IX_StringWrapper_ImagePathsId");

            migrationBuilder.CreateTable(
                name: "ImagePathsTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Preview = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagePathsTable", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_StringWrapper_ImagePathsTable_ImagePathsId",
                table: "StringWrapper",
                column: "ImagePathsId",
                principalTable: "ImagePathsTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StringWrapper_ImagePathsTable_ImagePathsId",
                table: "StringWrapper");

            migrationBuilder.DropTable(
                name: "ImagePathsTable");

            migrationBuilder.RenameColumn(
                name: "ImagePathsId",
                table: "StringWrapper",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_StringWrapper_ImagePathsId",
                table: "StringWrapper",
                newName: "IX_StringWrapper_ProductId");

            migrationBuilder.AddColumn<string>(
                name: "ImagePreviewName",
                table: "Information",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StringWrapper_Products_ProductId",
                table: "StringWrapper",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
