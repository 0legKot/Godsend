﻿// <copyright file="20180819120232_RemoveProductIdFromProperties.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RemoveProductIdFromProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
