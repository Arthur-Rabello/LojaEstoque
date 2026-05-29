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

        #region Listar
        public async Task<List<Produto>> Listar()
        {
            return await _servProduto.Listar();
        }
        #endregion

        #region BuscarPorId
        public async Task<Produto> BuscarPorId(Guid id)
        {
            return await _servProduto.BuscarPorId(id);
        }
        #endregion

        #region Remover
        public async Task<Produto> Remover(Guid id)
        {
            return await _servProduto.Remover(id);
        }
        #endregion

        #region Editar
        public async Task<Produto> Editar(Guid id, ProdutoDto produtoDto)
        {
            await _produtoValidator.ValidateAndThrowAsync(produtoDto);
            Produto produto = await _servProduto.Editar(id, produtoDto);
           
            return produto;

        }
        #endregion
    }
}
