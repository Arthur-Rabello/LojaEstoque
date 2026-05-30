using LojaEstoque.Dominio.Entidades;

namespace LojaEstoque.Repositories.Interfaces
{
    public interface IRepProduto
    {
        public Task<Produto?> Cadastrar(Produto produto);
        public Task<List<Produto?>> Listar();
        public Task<Produto?> BuscarPorId(Guid id);
        public Task<Produto?> Remover(Guid id);
        public Task<Produto?> Editar(Produto produto);
        public Task<bool> ExisteDescricao(string descricao);
        public Task<bool> ExisteDescricaoOutroProduto(Guid id, string descricao);
    }
}
