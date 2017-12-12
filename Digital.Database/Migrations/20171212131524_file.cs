using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Digital.Database.Migrations
{
    public partial class file : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ReceivedFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Filename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Filetype = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceivedFile", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_FileId",
                table: "Products",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ReceivedFile_FileId",
                table: "Products",
                column: "FileId",
                principalTable: "ReceivedFile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ReceivedFile_FileId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ReceivedFile");

            migrationBuilder.DropIndex(
                name: "IX_Products_FileId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Products");
        }
    }
}
