using AutoFixture.Xunit2;
using LojaEstoque.Dominio.Entidades;
using LojaEstoque.Dominio.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LojaEstoque.Tests
{
    public class ServTokenTests
    {
        #region GerarToken_UsuarioAdmin_DeveConterRoleAdmin
        [Theory]
        [AutoData]
        public void GerarToken_UsuarioAdmin_DeveConterRoleAdmin(Usuario usuario)
        {
            Environment.SetEnvironmentVariable("JWT_SECRET_KEY", "LojaEstoque_Chave_De_Teste_Com_Tamanho_Suficiente_123");
            Environment.SetEnvironmentVariable("JWT_ISSUER", "LojaEstoque");
            Environment.SetEnvironmentVariable("JWT_AUDIENCE", "LojaEstoqueUsuarios");
            Environment.SetEnvironmentVariable("JWT_EXPIRATION_MINUTES", "120");

            usuario.IsAdmin = true;

            ServToken servToken = new ServToken();

            string token = servToken.GerarToken(usuario);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

            Claim? roleClaim = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);

            Assert.False(string.IsNullOrWhiteSpace(token));
            Assert.NotNull(roleClaim);
            Assert.Equal("Admin", roleClaim.Value);
        }
        #endregion

        #region GerarToken_UsuarioComum_DeveConterRoleUsuario
        [Theory]
        [AutoData]
        public void GerarToken_UsuarioComum_DeveConterRoleUsuario(Usuario usuario)
        {
            Environment.SetEnvironmentVariable("JWT_SECRET_KEY", "LojaEstoque_Chave_De_Teste_Com_Tamanho_Suficiente_123");
            Environment.SetEnvironmentVariable("JWT_ISSUER", "LojaEstoque");
            Environment.SetEnvironmentVariable("JWT_AUDIENCE", "LojaEstoqueUsuarios");
            Environment.SetEnvironmentVariable("JWT_EXPIRATION_MINUTES", "120");

            usuario.IsAdmin = false;

            ServToken servToken = new ServToken();

            string token = servToken.GerarToken(usuario);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

            Claim? roleClaim = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);

            Assert.False(string.IsNullOrWhiteSpace(token));
            Assert.NotNull(roleClaim);
            Assert.Equal("Usuario", roleClaim.Value);
        }
        #endregion

        #region GerarToken_UsuarioValido_DeveConterDadosDoUsuario
        [Theory]
        [AutoData]
        public void GerarToken_UsuarioValido_DeveConterDadosDoUsuario(Usuario usuario)
        {
            Environment.SetEnvironmentVariable("JWT_SECRET_KEY", "LojaEstoque_Chave_De_Teste_Com_Tamanho_Suficiente_123");
            Environment.SetEnvironmentVariable("JWT_ISSUER", "LojaEstoque");
            Environment.SetEnvironmentVariable("JWT_AUDIENCE", "LojaEstoqueUsuarios");
            Environment.SetEnvironmentVariable("JWT_EXPIRATION_MINUTES", "120");

            ServToken servToken = new ServToken();

            string token = servToken.GerarToken(usuario);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

            Claim? idClaim = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            Claim? nomeClaim = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
            Claim? emailClaim = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);

            Assert.NotNull(idClaim);
            Assert.NotNull(nomeClaim);
            Assert.NotNull(emailClaim);

            Assert.Equal(usuario.Id.ToString(), idClaim.Value);
            Assert.Equal(usuario.Nome, nomeClaim.Value);
            Assert.Equal(usuario.Email, emailClaim.Value);
        }
        #endregion
    }
}