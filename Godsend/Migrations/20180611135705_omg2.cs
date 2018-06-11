using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Godsend.Migrations
{
    public partial class omg2 : Migration
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
