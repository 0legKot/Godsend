using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Godsend.Migrations
{
    public partial class images : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StringWrapper");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Thumb = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    ImagePathsId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_ImagePathsTable_ImagePathsId",
                        column: x => x.ImagePathsId,
                        principalTable: "ImagePathsTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_ImagePathsId",
                table: "Images",
                column: "ImagePathsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.CreateTable(
                name: "StringWrapper",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ImagePathsId = table.Column<Guid>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StringWrapper", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StringWrapper_ImagePathsTable_ImagePathsId",
                        column: x => x.ImagePathsId,
                        principalTable: "ImagePathsTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StringWrapper_ImagePathsId",
                table: "StringWrapper",
                column: "ImagePathsId");
        }
    }
}
