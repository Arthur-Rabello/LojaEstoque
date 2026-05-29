using FluentValidation;
using LojaEstoque.Aplicacao.Dtos;
using LojaEstoque.Aplicacao.Interfaces;
using LojaEstoque.Aplicacao.Validators;
using LojaEstoque.Dominio.Entidades;
using LojaEstoque.Dominio.Interfaces;
using LojaEstoque.Repositories.Interfaces;

namespace LojaEstoque.Aplicacao.Aplic
{
    public class AplicCarrinho : IAplicCarrinho
    {
        private readonly CarrinhoValidator _carrinhoValidator;
        private readonly IServCarrinho _servCarrinho;

        public AplicCarrinho(IServCarrinho servCarrinho)
        {
            _servCarrinho = servCarrinho;
            _carrinhoValidator = new CarrinhoValidator();
        }

        #region Cadastrar
        public async Task<Carrinho?> Cadastrar(CarrinhoDto carrinhoDto)
        {
            await _carrinhoValidator.ValidateAndThrowAsync(carrinhoDto);
            Carrinho carrinho = await _servCarrinho.Cadastrar(carrinhoDto);
            return carrinho;
        }
        #endregion

        #region Listar
        public async Task<List<Carrinho?>> Listar()
        {
            return await _servCarrinho.Listar();
        }
        #endregion

        #region BuscarPorId
        public async Task<Carrinho> BuscarPorId(Guid id)
        {
            return await _servCarrinho.BuscarPorId(id);
        }
        #endregion

        #region Remover
        public async Task<Carrinho> Remover(Guid id)
        {
            return await _servCarrinho.Remover(id);
        }
        #endregion

        #region Editar
        public async Task<Carrinho> Editar(Guid id, CarrinhoDto carrinhoDto)
        {
            await _carrinhoValidator.ValidateAndThrowAsync(carrinhoDto);
            Carrinho carrinho = await _servCarrinho.Editar(id, carrinhoDto);
            
            return carrinho;
        }
        #endregion
    }
}
