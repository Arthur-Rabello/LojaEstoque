using FluentValidation;
using LojaEstoque.Aplicacao.Dtos;
using LojaEstoque.Aplicacao.Interfaces;
using LojaEstoque.Aplicacao.Validators;
using LojaEstoque.Dominio.Entidades;
using LojaEstoque.Dominio.Interfaces;

namespace LojaEstoque.Aplicacao.Aplic
{
    public class AplicProduto : IAplicProduto
    {
        private readonly IServProduto _servProduto;
        private readonly ProdutoValidator _produtoValidator;

        #region AplicProduto
        public AplicProduto(IServProduto servProduto, ProdutoValidator produtoValidator)
        {
            _servProduto = servProduto;
            _produtoValidator = produtoValidator;
        }
        #endregion

        #region Cadastrar
        public async Task<Produto?> Cadastrar(ProdutoDto produtoDto)
        {
            await _produtoValidator.ValidateAndThrowAsync(produtoDto);

            Produto produto = await _servProduto.Cadastrar(produtoDto);

            return produto;
        }
        #endregion
    }
}
