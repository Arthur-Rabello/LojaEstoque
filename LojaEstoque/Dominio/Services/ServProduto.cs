using LojaEstoque.Dominio.Entidades;
using LojaEstoque.Repositories.Interfaces;
using LojaEstoque.Dominio.Exceptions;
using LojaEstoque.Dominio.Interfaces;
using LojaEstoque.Aplicacao.Dtos;

namespace LojaEstoque.Dominio.Services
{
    public class ServProduto : IServProduto
    {
        private readonly IRepProduto _irepProduto;

        #region ServProduto
        public ServProduto(IRepProduto irepProduto)
        {
            _irepProduto = irepProduto;
        }
        #endregion
        #region Cadastrar
        public async Task<Produto?> Cadastrar(ProdutoDto produtoDto)
        { 
            Produto produto = new Produto();

            produto.Descricao = produtoDto.Descricao;
            produto.PrecoUnitario = produtoDto.PrecoUnitario;
            produto.Quantidade = produtoDto.Quantidade;

            bool descricaoExiste = await _irepProduto.ExisteDescricao(produtoDto.Descricao);

            if (descricaoExiste)
            {
                throw new RegraDeNegocioException("Já existe um produto com essa descrição");
            }


            await _irepProduto.Cadastrar(produto);
            return produto;
        } 
        #endregion

        #region Listar
        public async Task<List<Produto>> Listar()
        {
            return await _irepProduto.Listar();

        }
        #endregion

        #region BuscarPorId
        public async Task<Produto> BuscarPorId(Guid id)
        {
            return await _irepProduto.BuscarPorId(id);
        }
        #endregion

        #region Remover
        public async Task<Produto> Remover(Guid id)
        {
            Produto produto = await _irepProduto.BuscarPorId(id);

            if (produto == null)
            {
                throw new RegraDeNegocioException("Produto não encontrado");
            }


            return await _irepProduto.Remover(id);
        }
        #endregion

        #region Editar
        public async Task<Produto> Editar(Guid id, ProdutoDto produtoDto)
        {
            Produto produto = await _irepProduto.BuscarPorId(id);

            if (produto == null)
            {
                throw new RegraDeNegocioException("Produto não encontrado");
            }

            produto.Descricao = produtoDto.Descricao;
            produto.PrecoUnitario = produtoDto.PrecoUnitario;
            produto.Quantidade = produtoDto.Quantidade;

            bool descricaoExiste = await _irepProduto.ExisteDescricaoOutroProduto(produto.Id, produtoDto.Descricao);

            if (descricaoExiste)
            {
                throw new RegraDeNegocioException("Já existe outro produto com essa descrição");
            }

            Produto produtoEditado = await _irepProduto.Editar(produto);

            return produtoEditado;
        }
        #endregion
    }

}