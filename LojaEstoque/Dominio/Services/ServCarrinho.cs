using LojaEstoque.Dominio.Interfaces;
using LojaEstoque.Dominio.Entidades;
using LojaEstoque.Aplicacao.Interfaces;
using LojaEstoque.Aplicacao.Dtos;
using LojaEstoque.Repositories.Interfaces;

namespace LojaEstoque.Dominio.Services
{
    public class ServCarrinho : IServCarrinho
    {
        private readonly IRepCarrinho _repCarrinho;
        private readonly IRepProduto _repProduto;

        public ServCarrinho(IRepProduto repProduto,IRepCarrinho repCarrinho)
        {
            _repCarrinho = repCarrinho;
            _repProduto = repProduto;
        }

        #region Cadastrar
        public async Task<Carrinho?> Cadastrar(CarrinhoDto carrinhoDto)
        {
            Produto? produto = await _repProduto.BuscarPorId(carrinhoDto.ProdutoId);

            Carrinho carrinho = new Carrinho();
            {
                carrinho.ProdutoId = produto.Id;
                carrinho.Quantidade = carrinhoDto.Quantidade;
                carrinho.PrecoUnitario = produto.PrecoUnitario;
                carrinho.PrecoTotal = carrinho.Quantidade * carrinho.PrecoUnitario;
            }
            ;
            await _repCarrinho.Cadastrar(carrinho);
            return carrinho;
        }
        #endregion

        #region Listar
        public async Task<List<Carrinho?>> Listar()
        {
                return await _repCarrinho.Listar();
        }

        #endregion

        #region BuscarPorId
        public async Task<Carrinho> BuscarPorId(Guid id)
        {
            return await _repCarrinho.BuscarPorId(id);
        }
        #endregion

        #region Remover
        public async Task<Carrinho> Remover(Guid id)
        {
            Carrinho carrinho = await _repCarrinho.BuscarPorId(id);
            if (carrinho == null)
            {
                throw new Exception("Carrinho não encontrado");
            }
            return await _repCarrinho.Remover(id);
        }
        #endregion

        #region Editar
        public async Task<Carrinho> Editar(Guid id, CarrinhoDto carrinhoDto)
        {
            Carrinho carrinho = await _repCarrinho.BuscarPorId(id);
            if (carrinho == null)
            {
                throw new Exception("Carrinho não encontrado");
            }
            {
                carrinho.ProdutoId = carrinho.ProdutoId;
                carrinho.Quantidade = carrinhoDto.Quantidade;
            }
            ;
            return await _repCarrinho.Editar(carrinho);
        }
        #endregion
    }
}
