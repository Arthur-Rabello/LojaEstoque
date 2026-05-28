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
    }
    #endregion
}