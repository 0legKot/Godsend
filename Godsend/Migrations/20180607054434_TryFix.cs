using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Godsend.Migrations
{
    public partial class TryFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ArticleInformation_InfoId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductInformation_InfoId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_StringWrapper_ArticleInformation_ArticleInformationId",
                table: "StringWrapper");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierInformation_Location_LocationId",
                table: "SupplierInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_SupplierInformation_InfoId",
                table: "Suppliers");

            migrationBuilder.DropTable(
                name: "ArticleInformation");

            migrationBuilder.DropTable(
                name: "ProductInformation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierInformation",
                table: "SupplierInformation");

            migrationBuilder.RenameTable(
                name: "SupplierInformation",
                newName: "Information");

            migrationBuilder.RenameIndex(
                name: "IX_SupplierInformation_LocationId",
                table: "Information",
                newName: "IX_Information_LocationId");

            migrationBuilder.AddColumn<Guid>(
                name: "EntityInformationId",
                table: "Suppliers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Information",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Information",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Information",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Information",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EntityInformationId",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EntityInformationId",
                table: "Articles",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Information",
                table: "Information",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_EntityInformationId",
                table: "Suppliers",
                column: "EntityInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_Information_AuthorId",
                table: "Information",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_EntityInformationId",
                table: "Products",
                column: "EntityInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_EntityInformationId",
                table: "Articles",
                column: "EntityInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Information_EntityInformationId",
                table: "Articles",
                column: "EntityInformationId",
                principalTable: "Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Information_InfoId",
                table: "Articles",
                column: "InfoId",
                principalTable: "Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Information_AspNetUsers_AuthorId",
                table: "Information",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Information_Location_LocationId",
                table: "Information",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Information_EntityInformationId",
                table: "Products",
                column: "EntityInformationId",
                principalTable: "Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Information_InfoId",
                table: "Products",
                column: "InfoId",
                principalTable: "Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StringWrapper_Information_ArticleInformationId",
                table: "StringWrapper",
                column: "ArticleInformationId",
                principalTable: "Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Information_EntityInformationId",
                table: "Suppliers",
                column: "EntityInformationId",
                principalTable: "Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Information_InfoId",
                table: "Suppliers",
                column: "InfoId",
                principalTable: "Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Information_EntityInformationId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Information_InfoId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Information_AspNetUsers_AuthorId",
                table: "Information");

            migrationBuilder.DropForeignKey(
                name: "FK_Information_Location_LocationId",
                table: "Information");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Information_EntityInformationId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Information_InfoId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_StringWrapper_Information_ArticleInformationId",
                table: "StringWrapper");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Information_EntityInformationId",
                table: "Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Information_InfoId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_EntityInformationId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Products_EntityInformationId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Information",
                table: "Information");

            migrationBuilder.DropIndex(
                name: "IX_Information_AuthorId",
                table: "Information");

            migrationBuilder.DropIndex(
                name: "IX_Articles_EntityInformationId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "EntityInformationId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "EntityInformationId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Information");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Information");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Information");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Information");

            migrationBuilder.DropColumn(
                name: "EntityInformationId",
                table: "Articles");

            migrationBuilder.RenameTable(
                name: "Information",
                newName: "SupplierInformation");

            migrationBuilder.RenameIndex(
                name: "IX_Information_LocationId",
                table: "SupplierInformation",
                newName: "IX_SupplierInformation_LocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierInformation",
                table: "SupplierInformation",
                column: "Id");

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
                name: "ProductInformation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Rating = table.Column<double>(nullable: false),
                    Watches = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInformation", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleInformation_AuthorId",
                table: "ArticleInformation",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_ArticleInformation_InfoId",
                table: "Articles",
                column: "InfoId",
                principalTable: "ArticleInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductInformation_InfoId",
                table: "Products",
                column: "InfoId",
                principalTable: "ProductInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StringWrapper_ArticleInformation_ArticleInformationId",
                table: "StringWrapper",
                column: "ArticleInformationId",
                principalTable: "ArticleInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierInformation_Location_LocationId",
                table: "SupplierInformation",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_SupplierInformation_InfoId",
                table: "Suppliers",
                column: "InfoId",
                principalTable: "SupplierInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
