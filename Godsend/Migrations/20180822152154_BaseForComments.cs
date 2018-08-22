using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Godsend.Migrations
{
    public partial class BaseForComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentsCount",
                table: "Information",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LinkCommentEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    BaseCommentId = table.Column<Guid>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    ArticleId = table.Column<Guid>(nullable: true),
                    ProductId = table.Column<Guid>(nullable: true),
                    SupplierId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkCommentEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkCommentEntity_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LinkCommentEntity_LinkCommentEntity_BaseCommentId",
                        column: x => x.BaseCommentId,
                        principalTable: "LinkCommentEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LinkCommentEntity_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LinkCommentEntity_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LinkCommentEntity_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinkCommentEntity_ArticleId",
                table: "LinkCommentEntity",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkCommentEntity_BaseCommentId",
                table: "LinkCommentEntity",
                column: "BaseCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkCommentEntity_UserId",
                table: "LinkCommentEntity",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkCommentEntity_ProductId",
                table: "LinkCommentEntity",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkCommentEntity_SupplierId",
                table: "LinkCommentEntity",
                column: "SupplierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinkCommentEntity");

            migrationBuilder.DropColumn(
                name: "CommentsCount",
                table: "Information");
        }
    }
}
