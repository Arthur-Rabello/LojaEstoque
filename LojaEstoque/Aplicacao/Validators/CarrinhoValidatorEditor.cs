using FluentValidation;
using LojaEstoque.Aplicacao.Dtos;

namespace LojaEstoque.Aplicacao.Validators
{
    public class CarrinhoValidatorEditor : AbstractValidator<CarrinhoEditarDto>
    {
        public CarrinhoValidatorEditor()
            {
                RuleFor(x => x.Quantidade)
                    .GreaterThan(0)
                    .WithMessage("A quantidade não pode ser negativa.");
        }
    }
}
