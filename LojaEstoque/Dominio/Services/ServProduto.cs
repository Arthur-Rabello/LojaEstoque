using LojaEstoque.Aplicacao.Aplic;
using LojaEstoque.Aplicacao.Interfaces;
using LojaEstoque.Dominio.Entidades;
using LojaEstoque.Repositories.Interfaces;
using LojaEstoque.Aplicacao.Validators;
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
                throw new Exception("Produto não encontrado");
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
                throw new Exception("Produto não encontrado");
            }

            produto.Descricao = produtoDto.Descricao;
            produto.PrecoUnitario = produtoDto.PrecoUnitario;
            produto.Quantidade = produtoDto.Quantidade;

            Produto produtoEditado = await _irepProduto.Editar(produto);

            return produtoEditado;
        }
        #endregion
    }

}