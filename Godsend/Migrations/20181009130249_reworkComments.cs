using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Godsend.Migrations
{
    public partial class reworkComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinkCommentEntity");

            migrationBuilder.CreateTable(
                name: "LinkCommentArticle",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    EntityId1 = table.Column<Guid>(nullable: true),
                    BaseCommentId = table.Column<Guid>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkCommentArticle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkCommentArticle_LinkCommentArticle_BaseCommentId",
                        column: x => x.BaseCommentId,
                        principalTable: "LinkCommentArticle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LinkCommentArticle_Articles_EntityId1",
                        column: x => x.EntityId1,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LinkCommentArticle_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LinkCommentProduct",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    EntityId1 = table.Column<Guid>(nullable: true),
                    BaseCommentId = table.Column<Guid>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkCommentProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkCommentProduct_LinkCommentProduct_BaseCommentId",
                        column: x => x.BaseCommentId,
                        principalTable: "LinkCommentProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LinkCommentProduct_Products_EntityId1",
                        column: x => x.EntityId1,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LinkCommentProduct_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LinkCommentSupplier",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    EntityId1 = table.Column<Guid>(nullable: true),
                    BaseCommentId = table.Column<Guid>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkCommentSupplier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkCommentSupplier_LinkCommentSupplier_BaseCommentId",
                        column: x => x.BaseCommentId,
                        principalTable: "LinkCommentSupplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LinkCommentSupplier_Suppliers_EntityId1",
                        column: x => x.EntityId1,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LinkCommentSupplier_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinkCommentArticle_BaseCommentId",
                table: "LinkCommentArticle",
                column: "BaseCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkCommentArticle_EntityId1",
                table: "LinkCommentArticle",
                column: "EntityId1");

            migrationBuilder.CreateIndex(
                name: "IX_LinkCommentArticle_UserId",
                table: "LinkCommentArticle",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkCommentProduct_BaseCommentId",
                table: "LinkCommentProduct",
                column: "BaseCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkCommentProduct_EntityId1",
                table: "LinkCommentProduct",
                column: "EntityId1");

            migrationBuilder.CreateIndex(
                name: "IX_LinkCommentProduct_UserId",
                table: "LinkCommentProduct",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkCommentSupplier_BaseCommentId",
                table: "LinkCommentSupplier",
                column: "BaseCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkCommentSupplier_EntityId1",
                table: "LinkCommentSupplier",
                column: "EntityId1");

            migrationBuilder.CreateIndex(
                name: "IX_LinkCommentSupplier_UserId",
                table: "LinkCommentSupplier",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinkCommentArticle");

            migrationBuilder.DropTable(
                name: "LinkCommentProduct");

            migrationBuilder.DropTable(
                name: "LinkCommentSupplier");

            migrationBuilder.CreateTable(
                name: "LinkCommentEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BaseCommentId = table.Column<Guid>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
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
    }
}
