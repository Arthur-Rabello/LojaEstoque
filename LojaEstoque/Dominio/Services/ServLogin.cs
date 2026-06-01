using LojaEstoque.Aplicacao.Dtos;
using LojaEstoque.Dominio.Entidades;
using LojaEstoque.Dominio.Interfaces;
using LojaEstoque.Repositories.Interfaces;

namespace LojaEstoque.Dominio.Services
{
    public class ServLogin : IServLogin
    {
        private readonly IRepUsuario _repUsuario;
        private readonly IServToken _servToken;

        public ServLogin(IRepUsuario repUsuario, IServToken servToken)
        {
            _repUsuario = repUsuario;
            _servToken = servToken;
        }

        #region Login
        public async Task<LoginRespostaDto> Login(LoginDto loginDto)
        {
            Usuario? usuario = await _repUsuario.BuscarPorEmail(loginDto.Email);

            if (usuario == null)
            {
                throw new Exception("E-mail ou senha inválidos.");
            }

            string token = _servToken.GerarToken(usuario);

            LoginRespostaDto loginRespostaDto = new LoginRespostaDto();

            loginRespostaDto.UsuarioId = usuario.Id;
            loginRespostaDto.Nome = usuario.Nome;
            loginRespostaDto.Email = usuario.Email;
            loginRespostaDto.IsAdmin = usuario.IsAdmin;
            loginRespostaDto.Token = token;

            return loginRespostaDto;
        }
        #endregion
    }
}
