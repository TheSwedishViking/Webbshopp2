using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webbshopp2.Migrations
{
    /// <inheritdoc />
    public partial class fraktval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "fraktAlternativ",
                table: "BeställdaProdukters",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fraktAlternativ",
                table: "BeställdaProdukters");
        }
    }
}
