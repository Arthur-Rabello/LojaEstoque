using LojaEstoque.Dominio.Entidades;

namespace LojaEstoque.Repositories.Interfaces
{
    public interface IRepProduto
    {
        public Task<Produto?> Cadastrar(Produto produto);
        public Task<List<Produto>> Listar();
        public Task<Produto?> BuscarPorId(Guid id);
        public Task<Produto> Remover(Guid id);
        public Task<Produto> Editar(Produto produto);
    }
}
