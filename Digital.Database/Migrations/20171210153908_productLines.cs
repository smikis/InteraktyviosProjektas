using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Digital.Database.Migrations
{
    public partial class productLines : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleLines_Carts_CartId",
                table: "SaleLines");

            migrationBuilder.DropIndex(
                name: "IX_SaleLines_CartId",
                table: "SaleLines");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "SaleLines");

            migrationBuilder.CreateTable(
                name: "ProductLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CartId = table.Column<int>(type: "int", nullable: true),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductLines_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductLines_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductLines_CartId",
                table: "ProductLines",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLines_ProductID",
                table: "ProductLines",
                column: "ProductID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductLines");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "SaleLines",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SaleLines_CartId",
                table: "SaleLines",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleLines_Carts_CartId",
                table: "SaleLines",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
