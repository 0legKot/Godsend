// <copyright file="20180729095214_renameInfo.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Migrations
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class renameInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Information_EntityInformationId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Information_EntityInformationId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Information_EntityInformationId",
                table: "Suppliers");

            migrationBuilder.RenameColumn(
                name: "EntityInformationId",
                table: "Suppliers",
                newName: "InfoId");

            migrationBuilder.RenameIndex(
                name: "IX_Suppliers_EntityInformationId",
                table: "Suppliers",
                newName: "IX_Suppliers_InfoId");

            migrationBuilder.RenameColumn(
                name: "EntityInformationId",
                table: "Products",
                newName: "InfoId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_EntityInformationId",
                table: "Products",
                newName: "IX_Products_InfoId");

            migrationBuilder.RenameColumn(
                name: "EntityInformationId",
                table: "Articles",
                newName: "InfoId");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_EntityInformationId",
                table: "Articles",
                newName: "IX_Articles_InfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Information_InfoId",
                table: "Articles",
                column: "InfoId",
                principalTable: "Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Information_InfoId",
                table: "Products",
                column: "InfoId",
                principalTable: "Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Information_InfoId",
                table: "Suppliers",
                column: "InfoId",
                principalTable: "Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Information_InfoId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Information_InfoId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Information_InfoId",
                table: "Suppliers");

            migrationBuilder.RenameColumn(
                name: "InfoId",
                table: "Suppliers",
                newName: "EntityInformationId");

            migrationBuilder.RenameIndex(
                name: "IX_Suppliers_InfoId",
                table: "Suppliers",
                newName: "IX_Suppliers_EntityInformationId");

            migrationBuilder.RenameColumn(
                name: "InfoId",
                table: "Products",
                newName: "EntityInformationId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_InfoId",
                table: "Products",
                newName: "IX_Products_EntityInformationId");

            migrationBuilder.RenameColumn(
                name: "InfoId",
                table: "Articles",
                newName: "EntityInformationId");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_InfoId",
                table: "Articles",
                newName: "IX_Articles_EntityInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Information_EntityInformationId",
                table: "Articles",
                column: "EntityInformationId",
                principalTable: "Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Information_EntityInformationId",
                table: "Products",
                column: "EntityInformationId",
                principalTable: "Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Information_EntityInformationId",
                table: "Suppliers",
                column: "EntityInformationId",
                principalTable: "Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
