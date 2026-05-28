using FluentValidation;
using LojaEstoque.Dominio.Entidades;
using LojaEstoque.Aplicacao.Dtos;

namespace LojaEstoque.Aplicacao.Validators
{
    public class ProdutoValidator : AbstractValidator<ProdutoDto>
    {
        #region ProdutoValidator
        public ProdutoValidator()
        {
            RuleFor(x => x.Descricao)
                .NotEmpty()
                .WithMessage("A descrição do produto é obrigatória.");

            RuleFor(x => x.Descricao)
                .MaximumLength(150)
                .WithMessage("A descrição do produto deve ter no máximo 150 caracteres.");

            RuleFor(x => x.PrecoUnitario)
                .GreaterThan(0)
                .WithMessage("O preço unitário deve ser maior que zero.");

            RuleFor(x => x.Quantidade)
                .GreaterThanOrEqualTo(0)
                .WithMessage("A quantidade não pode ser negativa.");
        }
        #endregion
    }
}