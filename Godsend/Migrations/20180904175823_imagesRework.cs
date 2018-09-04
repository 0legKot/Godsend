using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Godsend.Migrations
{
    public partial class imagesRework : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_ImagePathsTable_ImagePathsId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "ImagePathsTable");

            migrationBuilder.DropIndex(
                name: "IX_Images_ImagePathsId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ImagePathsId",
                table: "Images");

            migrationBuilder.AddColumn<Guid>(
                name: "PreviewId",
                table: "Suppliers",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PreviewId",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Images",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SupplierId",
                table: "Images",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_PreviewId",
                table: "Suppliers",
                column: "PreviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_PreviewId",
                table: "Products",
                column: "PreviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProductId",
                table: "Images",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_SupplierId",
                table: "Images",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Products_ProductId",
                table: "Images",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Suppliers_SupplierId",
                table: "Images",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Images_PreviewId",
                table: "Products",
                column: "PreviewId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Images_PreviewId",
                table: "Suppliers",
                column: "PreviewId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Products_ProductId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Suppliers_SupplierId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Images_PreviewId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Images_PreviewId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_PreviewId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Products_PreviewId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Images_ProductId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_SupplierId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "PreviewId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "PreviewId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "Images");

            migrationBuilder.AddColumn<Guid>(
                name: "ImagePathsId",
                table: "Images",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ImagePathsTable",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Preview = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagePathsTable", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_ImagePathsId",
                table: "Images",
                column: "ImagePathsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_ImagePathsTable_ImagePathsId",
                table: "Images",
                column: "ImagePathsId",
                principalTable: "ImagePathsTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
