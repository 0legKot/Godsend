namespace Godsend.Migrations
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class LinkAndRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Information_AspNetUsers_AuthorId",
                table: "Information");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_CustomerId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Orders",
                newName: "EFCustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                newName: "IX_Orders_EFCustomerId");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Information",
                newName: "EFAuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Information_AuthorId",
                table: "Information",
                newName: "IX_Information_EFAuthorId");

            migrationBuilder.CreateTable(
                name: "LinkProductsSuppliers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: true),
                    SupplierId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkProductsSuppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkProductsSuppliers_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LinkProductsSuppliers_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinkProductsSuppliers_ProductId",
                table: "LinkProductsSuppliers",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkProductsSuppliers_SupplierId",
                table: "LinkProductsSuppliers",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Information_AspNetUsers_EFAuthorId",
                table: "Information",
                column: "EFAuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_EFCustomerId",
                table: "Orders",
                column: "EFCustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Information_AspNetUsers_EFAuthorId",
                table: "Information");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_EFCustomerId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "LinkProductsSuppliers");

            migrationBuilder.RenameColumn(
                name: "EFCustomerId",
                table: "Orders",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_EFCustomerId",
                table: "Orders",
                newName: "IX_Orders_CustomerId");

            migrationBuilder.RenameColumn(
                name: "EFAuthorId",
                table: "Information",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Information_EFAuthorId",
                table: "Information",
                newName: "IX_Information_AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Information_AspNetUsers_AuthorId",
                table: "Information",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
