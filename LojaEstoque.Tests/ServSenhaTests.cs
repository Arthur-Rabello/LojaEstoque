using AutoFixture.Xunit2;
using LojaEstoque.Dominio.Services;

namespace LojaEstoque.Tests
{
	public class ServSenhaTests
	{
		#region GerarHash_SenhaValida_DeveRetornarHashDiferenteDaSenha
		[Theory]
		[AutoData]
		public void GerarHash_SenhaValida_DeveRetornarHashDiferenteDaSenha(string senha)
		{
			ServSenha servSenha = new ServSenha();

			string senhaHash = servSenha.GerarHash(senha);

			Assert.False(string.IsNullOrWhiteSpace(senhaHash));
			Assert.NotEqual(senha, senhaHash);
		}
		#endregion

		#region VerificarSenha_SenhaCorreta_DeveRetornarTrue
		[Theory]
		[AutoData]
		public void VerificarSenha_SenhaCorreta_DeveRetornarTrue(string senha)
		{
			ServSenha servSenha = new ServSenha();

			string senhaHash = servSenha.GerarHash(senha);

			bool senhaValida = servSenha.VerificarSenha(senha, senhaHash);

			Assert.True(senhaValida);
		}
		#endregion

		#region VerificarSenha_SenhaIncorreta_DeveRetornarFalse
		[Theory]
		[AutoData]
		public void VerificarSenha_SenhaIncorreta_DeveRetornarFalse(string senha, string senhaIncorreta)
		{
			ServSenha servSenha = new ServSenha();

			string senhaHash = servSenha.GerarHash(senha);

			bool senhaValida = servSenha.VerificarSenha(senhaIncorreta, senhaHash);

			Assert.False(senhaValida);
		}
		#endregion
	}
}