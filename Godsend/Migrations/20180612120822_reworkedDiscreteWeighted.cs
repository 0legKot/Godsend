using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Godsend.Migrations
{
    public partial class reworkedDiscreteWeighted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllData_Column_BaseColumnId",
                table: "AllData");

            migrationBuilder.DropForeignKey(
                name: "FK_AllData_Directory_DirId",
                table: "AllData");

            migrationBuilder.DropForeignKey(
                name: "FK_AllData_AllData_ValueId",
                table: "AllData");

            migrationBuilder.DropTable(
                name: "OrderPartDiscrete");

            migrationBuilder.DropTable(
                name: "OrderPartWeighted");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AllData",
                table: "AllData");

            migrationBuilder.RenameTable(
                name: "AllData",
                newName: "Values");

            migrationBuilder.RenameIndex(
                name: "IX_AllData_ValueId",
                table: "Values",
                newName: "IX_Values_ValueId");

            migrationBuilder.RenameIndex(
                name: "IX_AllData_DirId",
                table: "Values",
                newName: "IX_Values_DirId");

            migrationBuilder.RenameIndex(
                name: "IX_AllData_BaseColumnId",
                table: "Values",
                newName: "IX_Values_BaseColumnId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Values",
                table: "Values",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "OrderPartProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Multiplier = table.Column<int>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: true),
                    ProductId = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    SupplierId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPartProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderPartProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderPartProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderPartProducts_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderPartProducts_OrderId",
                table: "OrderPartProducts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPartProducts_ProductId",
                table: "OrderPartProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPartProducts_SupplierId",
                table: "OrderPartProducts",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Values_Column_BaseColumnId",
                table: "Values",
                column: "BaseColumnId",
                principalTable: "Column",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Values_Directory_DirId",
                table: "Values",
                column: "DirId",
                principalTable: "Directory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Values_Values_ValueId",
                table: "Values",
                column: "ValueId",
                principalTable: "Values",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Values_Column_BaseColumnId",
                table: "Values");

            migrationBuilder.DropForeignKey(
                name: "FK_Values_Directory_DirId",
                table: "Values");

            migrationBuilder.DropForeignKey(
                name: "FK_Values_Values_ValueId",
                table: "Values");

            migrationBuilder.DropTable(
                name: "OrderPartProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Values",
                table: "Values");

            migrationBuilder.RenameTable(
                name: "Values",
                newName: "AllData");

            migrationBuilder.RenameIndex(
                name: "IX_Values_ValueId",
                table: "AllData",
                newName: "IX_AllData_ValueId");

            migrationBuilder.RenameIndex(
                name: "IX_Values_DirId",
                table: "AllData",
                newName: "IX_AllData_DirId");

            migrationBuilder.RenameIndex(
                name: "IX_Values_BaseColumnId",
                table: "AllData",
                newName: "IX_AllData_BaseColumnId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AllData",
                table: "AllData",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "OrderPartDiscrete",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: true),
                    ProductId = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    SupplierId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPartDiscrete", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderPartDiscrete_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderPartDiscrete_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderPartDiscrete_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderPartWeighted",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: true),
                    ProductId = table.Column<Guid>(nullable: false),
                    SupplierId = table.Column<Guid>(nullable: false),
                    Weight = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPartWeighted", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderPartWeighted_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderPartWeighted_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderPartWeighted_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderPartDiscrete_OrderId",
                table: "OrderPartDiscrete",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPartDiscrete_ProductId",
                table: "OrderPartDiscrete",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPartDiscrete_SupplierId",
                table: "OrderPartDiscrete",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPartWeighted_OrderId",
                table: "OrderPartWeighted",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPartWeighted_ProductId",
                table: "OrderPartWeighted",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPartWeighted_SupplierId",
                table: "OrderPartWeighted",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_AllData_Column_BaseColumnId",
                table: "AllData",
                column: "BaseColumnId",
                principalTable: "Column",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AllData_Directory_DirId",
                table: "AllData",
                column: "DirId",
                principalTable: "Directory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AllData_AllData_ValueId",
                table: "AllData",
                column: "ValueId",
                principalTable: "AllData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
