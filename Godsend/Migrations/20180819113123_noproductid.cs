﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Godsend.Migrations
{
    public partial class noproductid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Properties",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Properties_ProductId",
                table: "Properties",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Products_ProductId",
                table: "Properties",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Products_ProductId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_ProductId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Properties");
        }
    }
}
