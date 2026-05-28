using LojaEstoque.Dominio.Entidades;

namespace LojaEstoque.Repositories.Interfaces
{
    public interface IRepProduto
    {
        public Task<Produto?> Cadastrar(Produto produto);
    }
}
