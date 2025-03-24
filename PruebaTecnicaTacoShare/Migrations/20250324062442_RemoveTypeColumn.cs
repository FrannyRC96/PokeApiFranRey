using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PruebaTecnicaTacoShare.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTypeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Pokemons");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Pokemons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
