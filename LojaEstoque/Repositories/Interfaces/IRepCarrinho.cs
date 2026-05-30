using LojaEstoque.Dominio.Entidades;

namespace LojaEstoque.Repositories.Interfaces
{
    public interface IRepCarrinho
    {
        public Task<Carrinho?> Cadastrar(Carrinho carrinho);
        public Task<List<Carrinho?>> Listar();
        public Task<Carrinho?> BuscarPorId(Guid id);
        public Task<Carrinho?> Remover(Guid id);
        public Task<Carrinho?> Editar(Carrinho carrinho);
    }
}
