using LojaEstoque.Aplicacao.Dtos;
using LojaEstoque.Dominio.Entidades;

namespace LojaEstoque.Dominio.Interfaces
{
    public interface IServCarrinho
    {
        public Task<Carrinho?> Cadastrar(CarrinhoDto carrinhoDto);
        public Task<CarrinhoResumoDto?> Listar();
        public Task<Carrinho?> BuscarPorId(Guid id);
        public Task<Carrinho> Remover(Guid id);
        public Task<Carrinho> Editar(Guid id, CarrinhoEditarDto carrinhoEditarDto);
    }
}
