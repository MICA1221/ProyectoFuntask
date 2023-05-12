using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FunTask.Migrations
{
    /// <inheritdoc />
    public partial class RelacionPadreHijo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hijos_AspNetUsers_UsuarioId",
                table: "Hijos");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Hijos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Hijos_AspNetUsers_UsuarioId",
                table: "Hijos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hijos_AspNetUsers_UsuarioId",
                table: "Hijos");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Hijos",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Hijos_AspNetUsers_UsuarioId",
                table: "Hijos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
