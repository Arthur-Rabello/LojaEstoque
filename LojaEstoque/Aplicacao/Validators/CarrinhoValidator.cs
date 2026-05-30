using FluentValidation;
using LojaEstoque.Aplicacao.Dtos;

namespace LojaEstoque.Aplicacao.Validators
{
    public class CarrinhoValidator : AbstractValidator<CarrinhoDto>
    {
        #region CarrinhoValidator
        public CarrinhoValidator()
        {
            RuleFor(x => x.ProdutoId)
                .NotEmpty()
                .WithMessage("O produto é obrigatório.");

            RuleFor(x => x.Quantidade)
                .GreaterThan(0)
                .WithMessage("A quantidade não pode ser negativa.");
        }
        #endregion
    }
}
