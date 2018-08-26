using Microsoft.EntityFrameworkCore.Migrations;

namespace Godsend.Migrations
{
    public partial class CascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LinkProductPropertyDecimal_Products_ProductId",
                table: "LinkProductPropertyDecimal");

            migrationBuilder.DropForeignKey(
                name: "FK_LinkProductPropertyInt_Products_ProductId",
                table: "LinkProductPropertyInt");

            migrationBuilder.DropForeignKey(
                name: "FK_LinkProductPropertyString_Products_ProductId",
                table: "LinkProductPropertyString");

            migrationBuilder.AddForeignKey(
                name: "FK_LinkProductPropertyDecimal_Products_ProductId",
                table: "LinkProductPropertyDecimal",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LinkProductPropertyInt_Products_ProductId",
                table: "LinkProductPropertyInt",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LinkProductPropertyString_Products_ProductId",
                table: "LinkProductPropertyString",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LinkProductPropertyDecimal_Products_ProductId",
                table: "LinkProductPropertyDecimal");

            migrationBuilder.DropForeignKey(
                name: "FK_LinkProductPropertyInt_Products_ProductId",
                table: "LinkProductPropertyInt");

            migrationBuilder.DropForeignKey(
                name: "FK_LinkProductPropertyString_Products_ProductId",
                table: "LinkProductPropertyString");

            migrationBuilder.AddForeignKey(
                name: "FK_LinkProductPropertyDecimal_Products_ProductId",
                table: "LinkProductPropertyDecimal",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LinkProductPropertyInt_Products_ProductId",
                table: "LinkProductPropertyInt",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LinkProductPropertyString_Products_ProductId",
                table: "LinkProductPropertyString",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
