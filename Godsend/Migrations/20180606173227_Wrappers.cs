// <copyright file="20180606173227_Wrappers.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Migrations
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class Wrappers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StringWrapper<string>");

            migrationBuilder.CreateTable(
                name: "StringWrapper",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ArticleInformationId = table.Column<Guid>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StringWrapper", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StringWrapper_ArticleInformation_ArticleInformationId",
                        column: x => x.ArticleInformationId,
                        principalTable: "ArticleInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StringWrapper_ArticleInformationId",
                table: "StringWrapper",
                column: "ArticleInformationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StringWrapper");

            migrationBuilder.CreateTable(
                name: "StringWrapper<string>",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ArticleInformationId = table.Column<Guid>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StringWrapper<string>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StringWrapper<string>_ArticleInformation_ArticleInformationId",
                        column: x => x.ArticleInformationId,
                        principalTable: "ArticleInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StringWrapper<string>_ArticleInformationId",
                table: "StringWrapper<string>",
                column: "ArticleInformationId");
        }
    }
}
