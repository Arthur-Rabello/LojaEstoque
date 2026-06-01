using FluentValidation;
using LojaEstoque.Aplicacao.Dtos;

namespace LojaEstoque.Aplicacao.Validators
{
    public class UsuarioEditarValidator : AbstractValidator<UsuarioEditarDto>
    {
        #region UsuarioEditarValidator
        public UsuarioEditarValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MinimumLength(4).WithMessage("O nome deve conter no mínimo 2 caracteres.")
                 .MaximumLength(100)
                .WithMessage("O nome deve conter no máximo 100 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O email é obrigatório.");

           RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("O e-mail informado é inválido.");
        }
        #endregion
    }
}
