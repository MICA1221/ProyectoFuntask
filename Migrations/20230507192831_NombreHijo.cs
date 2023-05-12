using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FunTask.Migrations
{
    /// <inheritdoc />
    public partial class NombreHijo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NombreHijo",
                table: "Hijos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NombreHijo",
                table: "Hijos");
        }
    }
}
