using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Godsend.Migrations
{
    public partial class tryCat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "StringWrapper",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StringWrapper_ProductId",
                table: "StringWrapper",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_StringWrapper_Products_ProductId",
                table: "StringWrapper",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StringWrapper_Products_ProductId",
                table: "StringWrapper");

            migrationBuilder.DropIndex(
                name: "IX_StringWrapper_ProductId",
                table: "StringWrapper");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "StringWrapper");
        }
    }
}
