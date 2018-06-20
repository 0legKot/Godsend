using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Godsend.Migrations
{
    public partial class AddDecimalPropType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LinkProductPropertyDecimal",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: true),
                    PropertyId = table.Column<Guid>(nullable: true),
                    Value = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkProductPropertyDecimal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkProductPropertyDecimal_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LinkProductPropertyDecimal_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinkProductPropertyDecimal_ProductId",
                table: "LinkProductPropertyDecimal",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkProductPropertyDecimal_PropertyId",
                table: "LinkProductPropertyDecimal",
                column: "PropertyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinkProductPropertyDecimal");
        }
    }
}
