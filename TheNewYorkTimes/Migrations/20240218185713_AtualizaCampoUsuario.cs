using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheNewYorkTimes.Migrations
{
    /// <inheritdoc />
    public partial class AtualizaCampoUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Usuario",
                newName: "Perfil");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Perfil",
                table: "Usuario",
                newName: "Role");
        }
    }
}
