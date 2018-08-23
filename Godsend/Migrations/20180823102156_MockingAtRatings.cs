using Microsoft.EntityFrameworkCore.Migrations;

namespace Godsend.Migrations
{
    public partial class MockingAtRatings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LinkRatingArticle_Articles_ArticleId",
                table: "LinkRatingArticle");

            migrationBuilder.DropForeignKey(
                name: "FK_LinkRatingProduct_Products_ProductId",
                table: "LinkRatingProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_LinkRatingSupplier_Suppliers_SupplierId",
                table: "LinkRatingSupplier");

            migrationBuilder.RenameColumn(
                name: "SupplierId",
                table: "LinkRatingSupplier",
                newName: "EntityId");

            migrationBuilder.RenameIndex(
                name: "IX_LinkRatingSupplier_SupplierId",
                table: "LinkRatingSupplier",
                newName: "IX_LinkRatingSupplier_EntityId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "LinkRatingProduct",
                newName: "EntityId");

            migrationBuilder.RenameIndex(
                name: "IX_LinkRatingProduct_ProductId",
                table: "LinkRatingProduct",
                newName: "IX_LinkRatingProduct_EntityId");

            migrationBuilder.RenameColumn(
                name: "ArticleId",
                table: "LinkRatingArticle",
                newName: "EntityId");

            migrationBuilder.RenameIndex(
                name: "IX_LinkRatingArticle_ArticleId",
                table: "LinkRatingArticle",
                newName: "IX_LinkRatingArticle_EntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_LinkRatingArticle_Articles_EntityId",
                table: "LinkRatingArticle",
                column: "EntityId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LinkRatingProduct_Products_EntityId",
                table: "LinkRatingProduct",
                column: "EntityId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LinkRatingSupplier_Suppliers_EntityId",
                table: "LinkRatingSupplier",
                column: "EntityId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LinkRatingArticle_Articles_EntityId",
                table: "LinkRatingArticle");

            migrationBuilder.DropForeignKey(
                name: "FK_LinkRatingProduct_Products_EntityId",
                table: "LinkRatingProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_LinkRatingSupplier_Suppliers_EntityId",
                table: "LinkRatingSupplier");

            migrationBuilder.RenameColumn(
                name: "EntityId",
                table: "LinkRatingSupplier",
                newName: "SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_LinkRatingSupplier_EntityId",
                table: "LinkRatingSupplier",
                newName: "IX_LinkRatingSupplier_SupplierId");

            migrationBuilder.RenameColumn(
                name: "EntityId",
                table: "LinkRatingProduct",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_LinkRatingProduct_EntityId",
                table: "LinkRatingProduct",
                newName: "IX_LinkRatingProduct_ProductId");

            migrationBuilder.RenameColumn(
                name: "EntityId",
                table: "LinkRatingArticle",
                newName: "ArticleId");

            migrationBuilder.RenameIndex(
                name: "IX_LinkRatingArticle_EntityId",
                table: "LinkRatingArticle",
                newName: "IX_LinkRatingArticle_ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_LinkRatingArticle_Articles_ArticleId",
                table: "LinkRatingArticle",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LinkRatingProduct_Products_ProductId",
                table: "LinkRatingProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LinkRatingSupplier_Suppliers_SupplierId",
                table: "LinkRatingSupplier",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
