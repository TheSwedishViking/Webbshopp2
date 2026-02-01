using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webbshopp2.Migrations
{
    /// <inheritdoc />
    public partial class AddCartItemNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ClothesId",
                table: "CartItems",
                column: "ClothesId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Clothes_ClothesId",
                table: "CartItems",
                column: "ClothesId",
                principalTable: "Clothes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Clothes_ClothesId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ClothesId",
                table: "CartItems");
        }
    }
}
