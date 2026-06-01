using FluentValidation;
using LojaEstoque.Aplicacao.Dtos;

namespace LojaEstoque.Aplicacao.Validators
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        #region LoginValidator
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O email é obrigatório.")
                .EmailAddress().WithMessage("O email deve ser válido.");

            RuleFor(x => x.Email).
                EmailAddress()
                .WithMessage("O email deve ser válido.");

            RuleFor(x => x.Senha)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(6).WithMessage("A senha deve conter no mínimo 6 caracteres.");
        }
        #endregion
    }
}
