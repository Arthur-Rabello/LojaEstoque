using FluentValidation;
using LojaEstoque.Aplicacao.Dtos;
using LojaEstoque.Aplicacao.Interfaces;
using LojaEstoque.Aplicacao.Validators;
using LojaEstoque.Dominio.Interfaces;

namespace LojaEstoque.Aplicacao.Aplic
{
    public class AplicLogin : IAplicLogin
    {
        private readonly IServLogin _servUsuario;
        private readonly LoginValidator _loginValidator;

        public AplicLogin(IServLogin servUsuario, LoginValidator loginValidator)
        {
            _servUsuario = servUsuario;
            _loginValidator = loginValidator;
        }

        #region Login
        public async Task<LoginRespostaDto> Login(LoginDto loginDto)
        {
            await _loginValidator.ValidateAndThrowAsync(loginDto);
            return await _servUsuario.Login(loginDto);
        }
        #endregion
    }
}
