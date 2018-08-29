using Microsoft.EntityFrameworkCore.Migrations;

namespace Godsend.Migrations
{
    public partial class uniqueTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "Tags",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Value",
                table: "Tags",
                column: "Value",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tags_Value",
                table: "Tags");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "Tags",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
