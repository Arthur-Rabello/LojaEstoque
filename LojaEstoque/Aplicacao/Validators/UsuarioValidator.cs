using FluentValidation;
using LojaEstoque.Aplicacao.Dtos;

namespace LojaEstoque.Aplicacao.Validators
{
    public class UsuarioValidator : AbstractValidator<UsuarioDto>
    {
        #region UsuarioValidator
        public UsuarioValidator() { 
            
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("O nome é obrigatório.");

            RuleFor(x => x.Nome)
                .MinimumLength(4)
                .WithMessage("O nome deve conter no mínimo 4 caracteres.")
                .MaximumLength(100)
                .WithMessage("O nome deve conter no máximo 100 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("O email é obrigatório.")
                .EmailAddress()
                .WithMessage("O email deve ser válido.");
            RuleFor(x => x.Senha)
                .NotEmpty()
                .WithMessage("A senha é obrigatória.")
                .MinimumLength(8)
                .WithMessage("A senha deve ter no mínimo 8 caracteres.")
                .Matches("[A-Z]")
                .WithMessage("A senha deve conter pelo menos uma letra maiúscula.")
                .Matches("[a-z]")
                .WithMessage("A senha deve conter pelo menos uma letra minúscula.")
                .Matches("[0-9]")
                .WithMessage("A senha deve conter pelo menos um número.")
                .Matches("[^a-zA-Z0-9]")
                .WithMessage("A senha deve conter pelo menos um caractere especial.");

        }
        #endregion
    }
}
