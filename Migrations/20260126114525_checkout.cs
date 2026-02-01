
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webbshopp2.Migrations
{
    /// <inheritdoc />
    public partial class checkout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BeställdaProdukters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeställdaProdukters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KöptaProdukters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    BeställningId = table.Column<int>(type: "int", nullable: false),
                    BoughtClothesId = table.Column<int>(type: "int", nullable: false),
                    ClothesId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoughtAntal = table.Column<int>(type: "int", nullable: false),
                    PriceAtPurchase = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KöptaProdukters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KöptaProdukters_BeställdaProdukters_BeställningId",
                        column: x => x.BeställningId,
                        principalTable: "BeställdaProdukters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KöptaProdukters_Clothes_ClothesId",
                        column: x => x.ClothesId,
                        principalTable: "Clothes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KöptaProdukters_BeställningId",
                table: "KöptaProdukters",
                column: "BeställningId");

            migrationBuilder.CreateIndex(
                name: "IX_KöptaProdukters_ClothesId",
                table: "KöptaProdukters",
                column: "ClothesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KöptaProdukters");

            migrationBuilder.DropTable(
                name: "BeställdaProdukters");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "CartItems");
        }
    }
}
