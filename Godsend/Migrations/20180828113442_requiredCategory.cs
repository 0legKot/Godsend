using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Godsend.Migrations
{
    public partial class requiredCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LinkProductPropertyDecimal_Properties_PropertyId",
                table: "LinkProductPropertyDecimal");

            migrationBuilder.DropForeignKey(
                name: "FK_LinkProductPropertyInt_Properties_PropertyId",
                table: "LinkProductPropertyInt");

            migrationBuilder.DropForeignKey(
                name: "FK_LinkProductPropertyString_Properties_PropertyId",
                table: "LinkProductPropertyString");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Products",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PropertyId",
                table: "LinkProductPropertyString",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "LinkProductPropertyString",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PropertyId",
                table: "LinkProductPropertyInt",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "LinkProductPropertyInt",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PropertyId",
                table: "LinkProductPropertyDecimal",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "LinkProductPropertyDecimal",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LinkProductPropertyDecimal_Properties_PropertyId",
                table: "LinkProductPropertyDecimal",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LinkProductPropertyInt_Properties_PropertyId",
                table: "LinkProductPropertyInt",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LinkProductPropertyString_Properties_PropertyId",
                table: "LinkProductPropertyString",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LinkProductPropertyDecimal_Properties_PropertyId",
                table: "LinkProductPropertyDecimal");

            migrationBuilder.DropForeignKey(
                name: "FK_LinkProductPropertyInt_Properties_PropertyId",
                table: "LinkProductPropertyInt");

            migrationBuilder.DropForeignKey(
                name: "FK_LinkProductPropertyString_Properties_PropertyId",
                table: "LinkProductPropertyString");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Products",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "PropertyId",
                table: "LinkProductPropertyString",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "LinkProductPropertyString",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "PropertyId",
                table: "LinkProductPropertyInt",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "LinkProductPropertyInt",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "PropertyId",
                table: "LinkProductPropertyDecimal",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "LinkProductPropertyDecimal",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_LinkProductPropertyDecimal_Properties_PropertyId",
                table: "LinkProductPropertyDecimal",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LinkProductPropertyInt_Properties_PropertyId",
                table: "LinkProductPropertyInt",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LinkProductPropertyString_Properties_PropertyId",
                table: "LinkProductPropertyString",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
