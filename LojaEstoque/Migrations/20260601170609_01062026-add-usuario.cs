using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaEstoque.Migrations
{
    /// <inheritdoc />
    public partial class _01062026addusuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecoUnitarioTotal",
                table: "Carrinho");

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Senha = table.Column<string>(type: "text", nullable: true),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.AddColumn<decimal>(
                name: "PrecoUnitarioTotal",
                table: "Carrinho",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
