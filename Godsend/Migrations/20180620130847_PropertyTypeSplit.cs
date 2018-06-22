// <copyright file="20180620130847_PropertyTypeSplit.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Migrations
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class PropertyTypeSplit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinkProductProperty");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Properties",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LinkProductPropertyInt",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: true),
                    PropertyId = table.Column<Guid>(nullable: true),
                    Value = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkProductPropertyInt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkProductPropertyInt_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LinkProductPropertyInt_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LinkProductPropertyString",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: true),
                    PropertyId = table.Column<Guid>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkProductPropertyString", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkProductPropertyString_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LinkProductPropertyString_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinkProductPropertyInt_ProductId",
                table: "LinkProductPropertyInt",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkProductPropertyInt_PropertyId",
                table: "LinkProductPropertyInt",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkProductPropertyString_ProductId",
                table: "LinkProductPropertyString",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkProductPropertyString_PropertyId",
                table: "LinkProductPropertyString",
                column: "PropertyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinkProductPropertyInt");

            migrationBuilder.DropTable(
                name: "LinkProductPropertyString");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Properties");

            migrationBuilder.CreateTable(
                name: "LinkProductProperty",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: true),
                    PropertyId = table.Column<Guid>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkProductProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkProductProperty_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LinkProductProperty_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinkProductProperty_ProductId",
                table: "LinkProductProperty",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkProductProperty_PropertyId",
                table: "LinkProductProperty",
                column: "PropertyId");
        }
    }
}
