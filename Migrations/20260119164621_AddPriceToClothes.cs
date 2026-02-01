using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webbshopp2.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceToClothes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Clothes",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Clothes");
        }
    }
}
