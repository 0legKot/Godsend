// <copyright file="20180801094812_fixRelations.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class fixRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Information_Products_ProductId",
                table: "Information");

            migrationBuilder.DropIndex(
                name: "IX_Information_ProductId",
                table: "Information");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Information");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Articles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Suppliers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Information",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Articles",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Articles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Information_ProductId",
                table: "Information",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Information_Products_ProductId",
                table: "Information",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
