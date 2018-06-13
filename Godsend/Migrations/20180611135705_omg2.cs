// <copyright file="20180611135705_omg2.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Migrations
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class Omg2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Data_Data_CellId",
                table: "Data");

            migrationBuilder.DropIndex(
                name: "IX_Data_CellId",
                table: "Data");

            migrationBuilder.RenameColumn(
                name: "CellId",
                table: "Data",
                newName: "RealDataId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Data",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Data_RealDataId",
                table: "Data",
                column: "RealDataId",
                unique: true,
                filter: "[RealDataId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Data_Data_RealDataId",
                table: "Data",
                column: "RealDataId",
                principalTable: "Data",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Data_Data_RealDataId",
                table: "Data");

            migrationBuilder.DropIndex(
                name: "IX_Data_RealDataId",
                table: "Data");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Data");

            migrationBuilder.RenameColumn(
                name: "RealDataId",
                table: "Data",
                newName: "CellId");

            migrationBuilder.CreateIndex(
                name: "IX_Data_CellId",
                table: "Data",
                column: "CellId");

            migrationBuilder.AddForeignKey(
                name: "FK_Data_Data_CellId",
                table: "Data",
                column: "CellId",
                principalTable: "Data",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
