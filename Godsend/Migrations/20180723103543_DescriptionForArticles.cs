using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Godsend.Migrations
{
    public partial class DescriptionForArticles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Information",
                newName: "ProductInformation_Description");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Information",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Information");

            migrationBuilder.RenameColumn(
                name: "ProductInformation_Description",
                table: "Information",
                newName: "Description");
        }
    }
}
