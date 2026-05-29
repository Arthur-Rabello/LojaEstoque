using LojaEstoque.Aplicacao.Dtos;
using LojaEstoque.Dominio.Entidades;

namespace LojaEstoque.Aplicacao.Interfaces
{
    public interface IAplicCarrinho
    {
        public Task<Carrinho?> Cadastrar(CarrinhoDto carrinhoDto);
        public Task<List<Carrinho?>> Listar();
        public Task<Carrinho?> BuscarPorId(Guid id);
        public Task<Carrinho> Remover(Guid id);
        public Task<Carrinho> Editar(Guid id, CarrinhoDto carrinhoDto);
    }
}
