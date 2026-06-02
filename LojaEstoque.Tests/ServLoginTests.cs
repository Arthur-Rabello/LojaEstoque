using AutoFixture.Xunit2;
using FakeItEasy;
using LojaEstoque.Aplicacao.Dtos;
using LojaEstoque.Dominio.Entidades;
using LojaEstoque.Dominio.Exceptions;
using LojaEstoque.Dominio.Interfaces;
using LojaEstoque.Dominio.Services;
using LojaEstoque.Repositories.Interfaces;

namespace LojaEstoque.Tests
{
    public class ServLoginTests
    {
        #region Login_EmailInexistente_DeveLancarRegraDeNegocioException
        [Theory]
        [AutoData]
        public async Task Login_EmailInexistente_DeveLancarRegraDeNegocioException(LoginDto loginDto)
        {
            IRepUsuario repUsuario = A.Fake<IRepUsuario>();
            IServToken servToken = A.Fake<IServToken>();
            IServSenha servSenha = A.Fake<IServSenha>();

            loginDto.Email = "teste@email.com";
            loginDto.Senha = "Senha123@";

            A.CallTo(() => repUsuario.BuscarPorEmail("teste@email.com"))
                .Returns(Task.FromResult<Usuario?>(null));

            ServLogin servLogin = new ServLogin(repUsuario, servToken, servSenha);

            RegraDeNegocioException exception = await Assert.ThrowsAsync<RegraDeNegocioException>(() => servLogin.Login(loginDto));

            Assert.Equal("E-mail ou senha inválidos.", exception.Message);

            A.CallTo(() => servToken.GerarToken(A<Usuario>._))
                .MustNotHaveHappened();
        }
        #endregion

        #region Login_SenhaInvalida_DeveLancarRegraDeNegocioException
        [Theory]
        [AutoData]
        public async Task Login_SenhaInvalida_DeveLancarRegraDeNegocioException(LoginDto loginDto, Usuario usuario)
        {
            IRepUsuario repUsuario = A.Fake<IRepUsuario>();
            IServToken servToken = A.Fake<IServToken>();
            IServSenha servSenha = A.Fake<IServSenha>();

            loginDto.Email = "teste@email.com";
            loginDto.Senha = "senha_errada";

            usuario.Email = "teste@email.com";
            usuario.SenhaHash = "hash_salvo";

            A.CallTo(() => repUsuario.BuscarPorEmail("teste@email.com"))
                .Returns(usuario);

            A.CallTo(() => servSenha.VerificarSenha("senha_errada", "hash_salvo"))
                .Returns(false);

            ServLogin servLogin = new ServLogin(repUsuario, servToken, servSenha);

            RegraDeNegocioException exception = await Assert.ThrowsAsync<RegraDeNegocioException>(() => servLogin.Login(loginDto));

            Assert.Equal("E-mail ou senha inválidos.", exception.Message);

            A.CallTo(() => servToken.GerarToken(A<Usuario>._))
                .MustNotHaveHappened();
        }
        #endregion

        #region Login_DadosValidos_DeveRetornarToken
        [Theory]
        [AutoData]
        public async Task Login_DadosValidos_DeveRetornarToken(LoginDto loginDto, Usuario usuario)
        {
            IRepUsuario repUsuario = A.Fake<IRepUsuario>();
            IServToken servToken = A.Fake<IServToken>();
            IServSenha servSenha = A.Fake<IServSenha>();

            loginDto.Email = "teste@email.com";
            loginDto.Senha = "senha_correta";

            usuario.Email = "teste@email.com";
            usuario.SenhaHash = "hash_salvo";
            usuario.IsAdmin = true;

            A.CallTo(() => repUsuario.BuscarPorEmail("teste@email.com"))
                .Returns(usuario);

            A.CallTo(() => servSenha.VerificarSenha("senha_correta", "hash_salvo"))
                .Returns(true);

            A.CallTo(() => servToken.GerarToken(usuario))
                .Returns("token_gerado");

            ServLogin servLogin = new ServLogin(repUsuario, servToken, servSenha);

            LoginRespostaDto loginRespostaDto = await servLogin.Login(loginDto);

            Assert.Equal(usuario.Id, loginRespostaDto.UsuarioId);
            Assert.Equal(usuario.Nome, loginRespostaDto.Nome);
            Assert.Equal(usuario.Email, loginRespostaDto.Email);
            Assert.Equal(usuario.IsAdmin, loginRespostaDto.IsAdmin);
            Assert.Equal("token_gerado", loginRespostaDto.Token);

            A.CallTo(() => servToken.GerarToken(usuario))
                .MustHaveHappenedOnceExactly();
        }
        #endregion

        #region Login_DadosValidos_DeveRetornarIsAdminCorreto
        [Theory]
        [AutoData]
        public async Task Login_DadosValidos_DeveRetornarIsAdminCorreto(LoginDto loginDto, Usuario usuario)
        {
            IRepUsuario repUsuario = A.Fake<IRepUsuario>();
            IServToken servToken = A.Fake<IServToken>();
            IServSenha servSenha = A.Fake<IServSenha>();

            loginDto.Email = "admin@email.com";
            loginDto.Senha = "senha_correta";

            usuario.Email = "admin@email.com";
            usuario.SenhaHash = "hash_salvo";
            usuario.IsAdmin = true;

            A.CallTo(() => repUsuario.BuscarPorEmail("admin@email.com"))
                .Returns(usuario);

            A.CallTo(() => servSenha.VerificarSenha("senha_correta", "hash_salvo"))
                .Returns(true);

            A.CallTo(() => servToken.GerarToken(usuario))
                .Returns("token_admin");

            ServLogin servLogin = new ServLogin(repUsuario, servToken, servSenha);

            LoginRespostaDto loginRespostaDto = await servLogin.Login(loginDto);

            Assert.True(loginRespostaDto.IsAdmin);
            Assert.Equal("token_admin", loginRespostaDto.Token);
        }
        #endregion
    }
}