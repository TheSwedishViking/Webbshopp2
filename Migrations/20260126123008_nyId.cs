using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webbshopp2.Migrations
{
    /// <inheritdoc />
    public partial class nyId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "KöptaProdukters");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "KöptaProdukters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
