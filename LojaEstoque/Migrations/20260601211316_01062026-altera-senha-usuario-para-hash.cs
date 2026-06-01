using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaEstoque.Migrations
{
    /// <inheritdoc />
    public partial class _01062026alterasenhausuarioparahash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Senha",
                table: "Usuario",
                newName: "SenhaHash");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SenhaHash",
                table: "Usuario",
                newName: "Senha");
        }
    }
}
