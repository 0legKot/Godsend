// <copyright file="20180723103543_DescriptionForArticles.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Migrations
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore.Migrations;

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
