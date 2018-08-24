namespace Godsend.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class EntityInfo121Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Information_InfoId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Information_InfoId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_StringWrapper_Information_ArticleInformationId",
                table: "StringWrapper");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Information_InfoId",
                table: "Suppliers");

            migrationBuilder.DropTable(
                name: "Information");

            migrationBuilder.DropIndex(
                name: "IX_Products_InfoId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Articles_InfoId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "InfoId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "InfoId",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "InfoId",
                table: "Suppliers",
                newName: "LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Suppliers_InfoId",
                table: "Suppliers",
                newName: "IX_Suppliers_LocationId");

            migrationBuilder.AddColumn<int>(
                name: "CommentsCount",
                table: "Suppliers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Suppliers",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Suppliers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Watches",
                table: "Suppliers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CommentsCount",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Products",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Watches",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CommentsCount",
                table: "Articles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Articles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Articles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EFAuthorId",
                table: "Articles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Articles",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Articles",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Watches",
                table: "Articles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_EFAuthorId",
                table: "Articles",
                column: "EFAuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_AspNetUsers_EFAuthorId",
                table: "Articles",
                column: "EFAuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StringWrapper_Articles_ArticleInformationId",
                table: "StringWrapper",
                column: "ArticleInformationId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Location_LocationId",
                table: "Suppliers",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_AspNetUsers_EFAuthorId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_StringWrapper_Articles_ArticleInformationId",
                table: "StringWrapper");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Location_LocationId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Articles_EFAuthorId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "CommentsCount",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Watches",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "CommentsCount",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Watches",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CommentsCount",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "EFAuthorId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "Watches",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "Suppliers",
                newName: "InfoId");

            migrationBuilder.RenameIndex(
                name: "IX_Suppliers_LocationId",
                table: "Suppliers",
                newName: "IX_Suppliers_InfoId");

            migrationBuilder.AddColumn<Guid>(
                name: "InfoId",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InfoId",
                table: "Articles",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Information",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CommentsCount = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Rating = table.Column<double>(nullable: false),
                    Watches = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    EFAuthorId = table.Column<string>(nullable: true),
                    ProductInformation_Description = table.Column<string>(nullable: true),
                    State = table.Column<int>(nullable: true),
                    LocationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Information", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Information_AspNetUsers_EFAuthorId",
                        column: x => x.EFAuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Information_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_InfoId",
                table: "Products",
                column: "InfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_InfoId",
                table: "Articles",
                column: "InfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Information_EFAuthorId",
                table: "Information",
                column: "EFAuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Information_LocationId",
                table: "Information",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Information_InfoId",
                table: "Articles",
                column: "InfoId",
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
                name: "FK_Suppliers_Information_InfoId",
                table: "Suppliers",
                column: "InfoId",
                principalTable: "Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
