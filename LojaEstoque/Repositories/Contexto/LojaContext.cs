using LojaEstoque.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace LojaEstoque.Repositories.Contexto
{
    public class LojaContext : DbContext
    {
        #region LojaContext
        public LojaContext(DbContextOptions<LojaContext> options) : base(options)
        {
        }
        #endregion

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Carrinho> Carrinho { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}