// <copyright file="20180603133007_privatesuicide.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Migrations
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class Privatesuicide : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "SupplierInformation",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupplierInformation_LocationId",
                table: "SupplierInformation",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierInformation_Location_LocationId",
                table: "SupplierInformation",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupplierInformation_Location_LocationId",
                table: "SupplierInformation");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropIndex(
                name: "IX_SupplierInformation_LocationId",
                table: "SupplierInformation");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "SupplierInformation");
        }
    }
}
