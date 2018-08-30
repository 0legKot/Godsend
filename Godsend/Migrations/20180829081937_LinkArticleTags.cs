using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Godsend.Migrations
{
    public partial class LinkArticleTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StringWrapper_Articles_ArticleInformationId",
                table: "StringWrapper");

            migrationBuilder.DropIndex(
                name: "IX_StringWrapper_ArticleInformationId",
                table: "StringWrapper");

            migrationBuilder.DropColumn(
                name: "ArticleInformationId",
                table: "StringWrapper");

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LinkArticleTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ArticleInfoId = table.Column<Guid>(nullable: false),
                    TagId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkArticleTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkArticleTags_Articles_ArticleInfoId",
                        column: x => x.ArticleInfoId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LinkArticleTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinkArticleTags_ArticleInfoId",
                table: "LinkArticleTags",
                column: "ArticleInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkArticleTags_TagId",
                table: "LinkArticleTags",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinkArticleTags");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.AddColumn<Guid>(
                name: "ArticleInformationId",
                table: "StringWrapper",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StringWrapper_ArticleInformationId",
                table: "StringWrapper",
                column: "ArticleInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_StringWrapper_Articles_ArticleInformationId",
                table: "StringWrapper",
                column: "ArticleInformationId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
