using LojaEstoque.Dominio.Entidades;
using LojaEstoque.Repositories.Interfaces;
using LojaEstoque.Dominio.Exceptions;
using LojaEstoque.Dominio.Interfaces;
using LojaEstoque.Aplicacao.Dtos;

namespace LojaEstoque.Dominio.Services
{
    public class ServProduto : IServProduto
    {
        private readonly IRepProduto _repProduto;

        #region ServProduto
        public ServProduto(IRepProduto irepProduto)
        {
            _repProduto = irepProduto;
        }
        #endregion
        #region Cadastrar
        public async Task<Produto?> Cadastrar(ProdutoDto produtoDto)
        { 
            Produto produto = new Produto();

            produto.Descricao = produtoDto.Descricao;
            produto.PrecoUnitario = produtoDto.PrecoUnitario;
            produto.Quantidade = produtoDto.Quantidade;

            bool descricaoExiste = await _repProduto.ExisteDescricao(produtoDto.Descricao);

            if (descricaoExiste)
            {
                throw new RegraDeNegocioException("Já existe um produto com essa descrição");
            }


            await _repProduto.Cadastrar(produto);
            return produto;
        } 
        #endregion

        #region Listar
        public async Task<List<Produto>> Listar()
        {
            return await _repProduto.Listar();

        }
        #endregion

        #region BuscarPorId
        public async Task<Produto> BuscarPorId(Guid id)
        {
            return await _repProduto.BuscarPorId(id);
        }
        #endregion

        #region Remover
        public async Task<Produto> Remover(Guid id)
        {
            Produto produto = await _repProduto.BuscarPorId(id);

            if (produto == null)
            {
                throw new RegraDeNegocioException("Produto não encontrado");
            }


            return await _repProduto.Remover(id);
        }
        #endregion

        #region Editar
        public async Task<Produto> Editar(Guid id, ProdutoDto produtoDto)
        {
            Produto produto = await _repProduto.BuscarPorId(id);

            if (produto == null)
            {
                throw new RegraDeNegocioException("Produto não encontrado");
            }

            produto.Descricao = produtoDto.Descricao;
            produto.PrecoUnitario = produtoDto.PrecoUnitario;
            produto.Quantidade = produtoDto.Quantidade;

            bool descricaoExiste = await _repProduto.ExisteDescricaoOutroProduto(produto.Id, produtoDto.Descricao);

            if (descricaoExiste)
            {
                throw new RegraDeNegocioException("Já existe outro produto com essa descrição");
            }

            Produto produtoEditado = await _repProduto.Editar(produto);

            return produtoEditado;
        }
        #endregion
    }

}