namespace Godsend.Migrations
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class Article : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleInformation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AuthorId = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Rating = table.Column<double>(nullable: false),
                    Watches = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleInformation_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    InfoId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_ArticleInformation_InfoId",
                        column: x => x.InfoId,
                        principalTable: "ArticleInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StringWrapper<string>",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ArticleInformationId = table.Column<Guid>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wrapper<string>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wrapper<string>_ArticleInformation_ArticleInformationId",
                        column: x => x.ArticleInformationId,
                        principalTable: "ArticleInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleInformation_AuthorId",
                table: "ArticleInformation",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_InfoId",
                table: "Articles",
                column: "InfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Wrapper<string>_ArticleInformationId",
                table: "StringWrapper<string>",
                column: "ArticleInformationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "StringWrapper<string>");

            migrationBuilder.DropTable(
                name: "ArticleInformation");
        }
    }
}
