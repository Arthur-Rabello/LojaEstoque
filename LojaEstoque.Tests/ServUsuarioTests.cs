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
	public class ServUsuarioTests
	{
		#region Cadastrar_EmailDuplicado_DeveLancarRegraDeNegocioException
		[Theory]
		[AutoData]
		public async Task Cadastrar_EmailDuplicado_DeveLancarRegraDeNegocioException(UsuarioDto usuarioDto)
		{
			IRepUsuario repUsuario = A.Fake<IRepUsuario>();
			IServToken servToken = A.Fake<IServToken>();
			IServSenha servSenha = A.Fake<IServSenha>();

			usuarioDto.Email = "Teste@Email.com";

			A.CallTo(() => repUsuario.ExistePorEmail("teste@email.com"))
				.Returns(true);

			ServUsuario servUsuario = new ServUsuario(repUsuario, servToken, servSenha);

			RegraDeNegocioException exception = await Assert.ThrowsAsync<RegraDeNegocioException>(() => servUsuario.Cadastrar(usuarioDto));

			Assert.Equal("Já existe um usuário com este email cadastrado", exception.Message);

			A.CallTo(() => repUsuario.Cadastrar(A<Usuario>._))
				.MustNotHaveHappened();
		}
		#endregion

		#region Cadastrar_EmailLivre_DeveCadastrarUsuarioComEmailNormalizado
		[Theory]
		[AutoData]
		public async Task Cadastrar_EmailLivre_DeveCadastrarUsuarioComEmailNormalizado(UsuarioDto usuarioDto)
		{
			IRepUsuario repUsuario = A.Fake<IRepUsuario>();
			IServToken servToken = A.Fake<IServToken>();
			IServSenha servSenha = A.Fake<IServSenha>();

			usuarioDto.Email = "  Arthur@Email.com  ";
			usuarioDto.Senha = "Teste123@";

			A.CallTo(() => repUsuario.ExistePorEmail("arthur@email.com"))
				.Returns(false);

			A.CallTo(() => servSenha.GerarHash(usuarioDto.Senha))
				.Returns("senha_hash");

			A.CallTo(() => repUsuario.Cadastrar(A<Usuario>._))
				.ReturnsLazily((Usuario usuario) => usuario);

			ServUsuario servUsuario = new ServUsuario(repUsuario, servToken, servSenha);

			Usuario? usuario = await servUsuario.Cadastrar(usuarioDto);

			Assert.NotNull(usuario);
			Assert.Equal("arthur@email.com", usuario.Email);
			Assert.Equal(usuarioDto.Nome, usuario.Nome);
			Assert.Equal("senha_hash", usuario.SenhaHash);
			Assert.False(usuario.IsAdmin);

			A.CallTo(() => repUsuario.Cadastrar(A<Usuario>.That.Matches(x =>
				x.Email == "arthur@email.com" &&
				x.SenhaHash == "senha_hash" &&
				x.IsAdmin == false)))
				.MustHaveHappenedOnceExactly();
		}
		#endregion

		#region Cadastrar_EmailLivre_DeveGerarSenhaHash
		[Theory]
		[AutoData]
		public async Task Cadastrar_EmailLivre_DeveGerarSenhaHash(UsuarioDto usuarioDto)
		{
			IRepUsuario repUsuario = A.Fake<IRepUsuario>();
			IServToken servToken = A.Fake<IServToken>();
			IServSenha servSenha = A.Fake<IServSenha>();

			usuarioDto.Email = "teste@email.com";
			usuarioDto.Senha = "Senha123@";

			A.CallTo(() => repUsuario.ExistePorEmail(usuarioDto.Email))
				.Returns(false);

			A.CallTo(() => servSenha.GerarHash(usuarioDto.Senha))
				.Returns("hash_gerado");

			A.CallTo(() => repUsuario.Cadastrar(A<Usuario>._))
				.ReturnsLazily((Usuario usuario) => usuario);

			ServUsuario servUsuario = new ServUsuario(repUsuario, servToken, servSenha);

			Usuario? usuario = await servUsuario.Cadastrar(usuarioDto);

			Assert.NotNull(usuario);
			Assert.Equal("hash_gerado", usuario.SenhaHash);

			A.CallTo(() => servSenha.GerarHash(usuarioDto.Senha))
				.MustHaveHappenedOnceExactly();
		}
		#endregion

		#region Editar_UsuarioInexistente_DeveLancarRegraDeNegocioException
		[Theory]
		[AutoData]
		public async Task Editar_UsuarioInexistente_DeveLancarRegraDeNegocioException(Guid id, UsuarioEditarDto usuarioEditarDto)
		{
			IRepUsuario repUsuario = A.Fake<IRepUsuario>();
			IServToken servToken = A.Fake<IServToken>();
			IServSenha servSenha = A.Fake<IServSenha>();

			A.CallTo(() => repUsuario.BuscarPorId(id))
				.Returns(Task.FromResult<Usuario?>(null));

			ServUsuario servUsuario = new ServUsuario(repUsuario, servToken, servSenha);

			RegraDeNegocioException exception = await Assert.ThrowsAsync<RegraDeNegocioException>(() => servUsuario.Editar(id, usuarioEditarDto));

			Assert.Equal("Usuário não encontrado.", exception.Message);

			A.CallTo(() => repUsuario.Editar(A<Usuario>._))
				.MustNotHaveHappened();
		}
		#endregion

		#region Editar_EmailJaCadastrado_DeveLancarRegraDeNegocioException
		[Theory]
		[AutoData]
		public async Task Editar_EmailJaCadastrado_DeveLancarRegraDeNegocioException(Guid id, Usuario usuario, UsuarioEditarDto usuarioEditarDto)
		{
			IRepUsuario repUsuario = A.Fake<IRepUsuario>();
			IServToken servToken = A.Fake<IServToken>();
			IServSenha servSenha = A.Fake<IServSenha>();

			usuario.Id = id;
			usuario.Email = "emailantigo@email.com";
			usuarioEditarDto.Email = "emailnovo@email.com";

			A.CallTo(() => repUsuario.BuscarPorId(id))
				.Returns(usuario);

			A.CallTo(() => repUsuario.ExistePorEmail(usuarioEditarDto.Email))
				.Returns(true);

			ServUsuario servUsuario = new ServUsuario(repUsuario, servToken, servSenha);

			RegraDeNegocioException exception = await Assert.ThrowsAsync<RegraDeNegocioException>(() => servUsuario.Editar(id, usuarioEditarDto));

			Assert.Equal("Email já cadastrado.", exception.Message);

			A.CallTo(() => repUsuario.Editar(A<Usuario>._))
				.MustNotHaveHappened();
		}
		#endregion

		#region Editar_DadosValidos_DeveEditarUsuario
		[Theory]
		[AutoData]
		public async Task Editar_DadosValidos_DeveEditarUsuario(Guid id, Usuario usuario, UsuarioEditarDto usuarioEditarDto)
		{
			IRepUsuario repUsuario = A.Fake<IRepUsuario>();
			IServToken servToken = A.Fake<IServToken>();
			IServSenha servSenha = A.Fake<IServSenha>();

			usuario.Id = id;
			usuario.Email = "emailantigo@email.com";

			usuarioEditarDto.Nome = "Novo Nome";
			usuarioEditarDto.Email = "emailnovo@email.com";

			A.CallTo(() => repUsuario.BuscarPorId(id))
				.Returns(usuario);

			A.CallTo(() => repUsuario.ExistePorEmail(usuarioEditarDto.Email))
				.Returns(false);

			A.CallTo(() => repUsuario.Editar(A<Usuario>._))
				.ReturnsLazily((Usuario usuarioEditado) => usuarioEditado);

			ServUsuario servUsuario = new ServUsuario(repUsuario, servToken, servSenha);

			Usuario usuarioResultado = await servUsuario.Editar(id, usuarioEditarDto);

			Assert.Equal(usuarioEditarDto.Nome, usuarioResultado.Nome);
			Assert.Equal(usuarioEditarDto.Email, usuarioResultado.Email);

			A.CallTo(() => repUsuario.Editar(A<Usuario>.That.Matches(x =>
				x.Id == id &&
				x.Nome == usuarioEditarDto.Nome &&
				x.Email == usuarioEditarDto.Email)))
				.MustHaveHappenedOnceExactly();
		}
		#endregion

		#region TornarAdmin_UsuarioInexistente_DeveLancarRegraDeNegocioException
		[Theory]
		[AutoData]
		public async Task TornarAdmin_UsuarioInexistente_DeveLancarRegraDeNegocioException(Guid id)
		{
			IRepUsuario repUsuario = A.Fake<IRepUsuario>();
			IServToken servToken = A.Fake<IServToken>();
			IServSenha servSenha = A.Fake<IServSenha>();

			A.CallTo(() => repUsuario.BuscarPorId(id))
				.Returns(Task.FromResult<Usuario?>(null));

			ServUsuario servUsuario = new ServUsuario(repUsuario, servToken, servSenha);

			RegraDeNegocioException exception = await Assert.ThrowsAsync<RegraDeNegocioException>(() => servUsuario.TornarAdmin(id));

			Assert.Equal("Usuário não encontrado.", exception.Message);

			A.CallTo(() => repUsuario.Editar(A<Usuario>._))
				.MustNotHaveHappened();
		}
		#endregion

		#region TornarAdmin_UsuarioExistente_DeveAlterarIsAdminParaTrue
		[Theory]
		[AutoData]
		public async Task TornarAdmin_UsuarioExistente_DeveAlterarIsAdminParaTrue(Guid id, Usuario usuario)
		{
			IRepUsuario repUsuario = A.Fake<IRepUsuario>();
			IServToken servToken = A.Fake<IServToken>();
			IServSenha servSenha = A.Fake<IServSenha>();

			usuario.Id = id;
			usuario.IsAdmin = false;

			A.CallTo(() => repUsuario.BuscarPorId(id))
				.Returns(usuario);

			A.CallTo(() => repUsuario.Editar(A<Usuario>._))
				.ReturnsLazily((Usuario usuarioEditado) => usuarioEditado);

			ServUsuario servUsuario = new ServUsuario(repUsuario, servToken, servSenha);

			Usuario usuarioResultado = await servUsuario.TornarAdmin(id);

			Assert.True(usuarioResultado.IsAdmin);

			A.CallTo(() => repUsuario.Editar(A<Usuario>.That.Matches(x =>
				x.Id == id &&
				x.IsAdmin == true)))
				.MustHaveHappenedOnceExactly();
		}
		#endregion

		#region AlterarSenha_SenhaAtualInvalida_DeveLancarException
		[Theory]
		[AutoData]
		public async Task AlterarSenha_SenhaAtualInvalida_DeveLancarException(Guid id, Usuario usuario, UsuarioAlterarSenhaDto usuarioAlterarSenhaDto)
		{
			IRepUsuario repUsuario = A.Fake<IRepUsuario>();
			IServToken servToken = A.Fake<IServToken>();
			IServSenha servSenha = A.Fake<IServSenha>();

			usuario.Id = id;
			usuario.SenhaHash = "hash_antigo";

			usuarioAlterarSenhaDto.SenhaAtual = "senha_errada";
			usuarioAlterarSenhaDto.NovaSenha = "senha_nova";

			A.CallTo(() => repUsuario.BuscarPorId(id))
				.Returns(usuario);

			A.CallTo(() => servSenha.VerificarSenha(usuarioAlterarSenhaDto.SenhaAtual, usuario.SenhaHash))
				.Returns(false);

			ServUsuario servUsuario = new ServUsuario(repUsuario, servToken, servSenha);

			Exception exception = await Assert.ThrowsAsync<Exception>(() => servUsuario.AlterarSenha(id, usuarioAlterarSenhaDto));

			Assert.Equal("Senha atual inválida.", exception.Message);

			A.CallTo(() => repUsuario.Editar(A<Usuario>._))
				.MustNotHaveHappened();
		}
		#endregion

		#region AlterarSenha_SenhaAtualValida_DeveGerarNovaSenhaHash
		[Theory]
		[AutoData]
		public async Task AlterarSenha_SenhaAtualValida_DeveGerarNovaSenhaHash(Guid id, Usuario usuario, UsuarioAlterarSenhaDto usuarioAlterarSenhaDto)
		{
			IRepUsuario repUsuario = A.Fake<IRepUsuario>();
			IServToken servToken = A.Fake<IServToken>();
			IServSenha servSenha = A.Fake<IServSenha>();

			usuario.Id = id;
			usuario.SenhaHash = "hash_antigo";

			usuarioAlterarSenhaDto.SenhaAtual = "senha_antiga";
			usuarioAlterarSenhaDto.NovaSenha = "senha_nova";

			A.CallTo(() => repUsuario.BuscarPorId(id))
				.Returns(usuario);

			A.CallTo(() => servSenha.VerificarSenha(usuarioAlterarSenhaDto.SenhaAtual, usuario.SenhaHash))
				.Returns(true);

			A.CallTo(() => servSenha.GerarHash(usuarioAlterarSenhaDto.NovaSenha))
				.Returns("hash_novo");

			A.CallTo(() => repUsuario.Editar(A<Usuario>._))
				.ReturnsLazily((Usuario usuarioEditado) => usuarioEditado);

			ServUsuario servUsuario = new ServUsuario(repUsuario, servToken, servSenha);

			Usuario usuarioResultado = await servUsuario.AlterarSenha(id, usuarioAlterarSenhaDto);

			Assert.Equal("hash_novo", usuarioResultado.SenhaHash);

			A.CallTo(() => repUsuario.Editar(A<Usuario>.That.Matches(x =>
				x.Id == id &&
				x.SenhaHash == "hash_novo")))
				.MustHaveHappenedOnceExactly();
		}
		#endregion
	}
}