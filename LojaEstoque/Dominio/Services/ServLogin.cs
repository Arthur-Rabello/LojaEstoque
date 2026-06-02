using LojaEstoque.Aplicacao.Dtos;
using LojaEstoque.Dominio.Entidades;
using LojaEstoque.Dominio.Exceptions;
using LojaEstoque.Dominio.Interfaces;
using LojaEstoque.Repositories.Interfaces;

namespace LojaEstoque.Dominio.Services
{
    public class ServLogin : IServLogin
    {
        private readonly IRepUsuario _repUsuario;
        private readonly IServToken _servToken;
        private readonly IServSenha _servSenha;

        public ServLogin(IRepUsuario repUsuario, IServToken servToken, IServSenha servSenha)
        {
            _repUsuario = repUsuario;
            _servToken = servToken;
            _servSenha = servSenha;
        }

        #region Login
        public async Task<LoginRespostaDto> Login(LoginDto loginDto)
        {
            string emailnormalizado = loginDto.Email.Trim().ToLowerInvariant();
            Usuario? usuario = await _repUsuario.BuscarPorEmail(emailnormalizado);

            if (usuario == null)
            {
                throw new RegraDeNegocioException("E-mail ou senha inválidos.");
            }

            bool senhavalida = _servSenha.VerificarSenha(loginDto.Senha, usuario.SenhaHash);

            if (senhavalida == false)
            {
                throw new RegraDeNegocioException("E-mail ou senha inválidos.");
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
