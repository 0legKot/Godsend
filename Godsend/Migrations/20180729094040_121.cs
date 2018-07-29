using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Godsend.Migrations
{
    public partial class _121 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropTable(
                name: "Values");

            migrationBuilder.DropTable(
                name: "Column");

            migrationBuilder.DropTable(
                name: "Directory");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_InfoId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Products_InfoId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Articles_InfoId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "InfoId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "InfoId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "InfoId",
                table: "Articles");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Information",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Information_Products_ProductId",
                table: "Information");

            migrationBuilder.DropIndex(
                name: "IX_Information_ProductId",
                table: "Information");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Information");

            migrationBuilder.AddColumn<Guid>(
                name: "InfoId",
                table: "Suppliers",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InfoId",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InfoId",
                table: "Articles",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Directory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BaseId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Directory_Directory_BaseId",
                        column: x => x.BaseId,
                        principalTable: "Directory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Column",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BaseClassId = table.Column<int>(nullable: true),
                    ColumnType = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Column", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Column_Directory_BaseClassId",
                        column: x => x.BaseClassId,
                        principalTable: "Directory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Values",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BaseColumnId = table.Column<int>(nullable: true),
                    DirId = table.Column<int>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    ValueId = table.Column<int>(nullable: true),
                    RealData = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Values", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Values_Column_BaseColumnId",
                        column: x => x.BaseColumnId,
                        principalTable: "Column",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Values_Directory_DirId",
                        column: x => x.DirId,
                        principalTable: "Directory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Values_Values_ValueId",
                        column: x => x.ValueId,
                        principalTable: "Values",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_InfoId",
                table: "Suppliers",
                column: "InfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_InfoId",
                table: "Products",
                column: "InfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_InfoId",
                table: "Articles",
                column: "InfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Column_BaseClassId",
                table: "Column",
                column: "BaseClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Directory_BaseId",
                table: "Directory",
                column: "BaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Values_BaseColumnId",
                table: "Values",
                column: "BaseColumnId");

            migrationBuilder.CreateIndex(
                name: "IX_Values_DirId",
                table: "Values",
                column: "DirId");

            migrationBuilder.CreateIndex(
                name: "IX_Values_ValueId",
                table: "Values",
                column: "ValueId");

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
    }
}
