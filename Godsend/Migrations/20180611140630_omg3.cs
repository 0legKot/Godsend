using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Godsend.Migrations
{
    public partial class omg3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Column_DirList_DirectoryId",
                table: "Column");

            migrationBuilder.DropForeignKey(
                name: "FK_Data_Column_ColumnId",
                table: "Data");

            migrationBuilder.DropForeignKey(
                name: "FK_Data_Data_RealDataId",
                table: "Data");

            migrationBuilder.DropForeignKey(
                name: "FK_DirList_DirList_BaseId",
                table: "DirList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DirList",
                table: "DirList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Data",
                table: "Data");

            migrationBuilder.DropIndex(
                name: "IX_Data_RealDataId",
                table: "Data");

            migrationBuilder.RenameTable(
                name: "DirList",
                newName: "Directory");

            migrationBuilder.RenameTable(
                name: "Data",
                newName: "AllData");

            migrationBuilder.RenameIndex(
                name: "IX_DirList_BaseId",
                table: "Directory",
                newName: "IX_Directory_BaseId");

            migrationBuilder.RenameColumn(
                name: "RealDataId",
                table: "AllData",
                newName: "ValueId");

            migrationBuilder.RenameColumn(
                name: "ColumnId",
                table: "AllData",
                newName: "BaseColumnId");

            migrationBuilder.RenameIndex(
                name: "IX_Data_ColumnId",
                table: "AllData",
                newName: "IX_AllData_BaseColumnId");

            migrationBuilder.RenameColumn(
                name: "DirectoryId",
                table: "Column",
                newName: "BaseClassId");

            migrationBuilder.RenameIndex(
                name: "IX_Column_DirectoryId",
                table: "Column",
                newName: "IX_Column_BaseClassId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Directory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RealData",
                table: "AllData",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Directory",
                table: "Directory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AllData",
                table: "AllData",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AllData_ValueId",
                table: "AllData",
                column: "ValueId",
                unique: true,
                filter: "[ValueId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AllData_Column_BaseColumnId",
                table: "AllData",
                column: "BaseColumnId",
                principalTable: "Column",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AllData_AllData_ValueId",
                table: "AllData",
                column: "ValueId",
                principalTable: "AllData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Column_Directory_BaseClassId",
                table: "Column",
                column: "BaseClassId",
                principalTable: "Directory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Directory_Directory_BaseId",
                table: "Directory",
                column: "BaseId",
                principalTable: "Directory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllData_Column_BaseColumnId",
                table: "AllData");

            migrationBuilder.DropForeignKey(
                name: "FK_AllData_AllData_ValueId",
                table: "AllData");

            migrationBuilder.DropForeignKey(
                name: "FK_Column_Directory_BaseClassId",
                table: "Column");

            migrationBuilder.DropForeignKey(
                name: "FK_Directory_Directory_BaseId",
                table: "Directory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Directory",
                table: "Directory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AllData",
                table: "AllData");

            migrationBuilder.DropIndex(
                name: "IX_AllData_ValueId",
                table: "AllData");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Directory");

            migrationBuilder.DropColumn(
                name: "RealData",
                table: "AllData");

            migrationBuilder.RenameTable(
                name: "Directory",
                newName: "DirList");

            migrationBuilder.RenameTable(
                name: "AllData",
                newName: "Data");

            migrationBuilder.RenameIndex(
                name: "IX_Directory_BaseId",
                table: "DirList",
                newName: "IX_DirList_BaseId");

            migrationBuilder.RenameColumn(
                name: "BaseClassId",
                table: "Column",
                newName: "DirectoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Column_BaseClassId",
                table: "Column",
                newName: "IX_Column_DirectoryId");

            migrationBuilder.RenameColumn(
                name: "ValueId",
                table: "Data",
                newName: "RealDataId");

            migrationBuilder.RenameColumn(
                name: "BaseColumnId",
                table: "Data",
                newName: "ColumnId");

            migrationBuilder.RenameIndex(
                name: "IX_AllData_BaseColumnId",
                table: "Data",
                newName: "IX_Data_ColumnId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DirList",
                table: "DirList",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Data",
                table: "Data",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Data_RealDataId",
                table: "Data",
                column: "RealDataId",
                unique: true,
                filter: "[RealDataId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Column_DirList_DirectoryId",
                table: "Column",
                column: "DirectoryId",
                principalTable: "DirList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Data_Column_ColumnId",
                table: "Data",
                column: "ColumnId",
                principalTable: "Column",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Data_Data_RealDataId",
                table: "Data",
                column: "RealDataId",
                principalTable: "Data",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirList_DirList_BaseId",
                table: "DirList",
                column: "BaseId",
                principalTable: "DirList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
