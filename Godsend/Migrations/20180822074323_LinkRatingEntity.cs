// <copyright file="20180822074323_LinkRatingEntity.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class LinkRatingEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LinkRatingArticle",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    ArticleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkRatingArticle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkRatingArticle_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LinkRatingArticle_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LinkRatingProduct",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    ProductId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkRatingProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkRatingProduct_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LinkRatingProduct_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LinkRatingSupplier",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    SupplierId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkRatingSupplier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkRatingSupplier_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LinkRatingSupplier_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinkRatingArticle_ArticleId",
                table: "LinkRatingArticle",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkRatingArticle_UserId",
                table: "LinkRatingArticle",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkRatingProduct_ProductId",
                table: "LinkRatingProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkRatingProduct_UserId",
                table: "LinkRatingProduct",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkRatingSupplier_SupplierId",
                table: "LinkRatingSupplier",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkRatingSupplier_UserId",
                table: "LinkRatingSupplier",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinkRatingArticle");

            migrationBuilder.DropTable(
                name: "LinkRatingProduct");

            migrationBuilder.DropTable(
                name: "LinkRatingSupplier");
        }
    }
}
