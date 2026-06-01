using LojaEstoque.Dominio.Entidades;
using LojaEstoque.Dominio.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LojaEstoque.Dominio.Services
{
    public class ServToken : IServToken
    {
        #region GerarToken
        public string GerarToken(Usuario usuario)
        {
            string jwtSecretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
            string jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
            string jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
            string jwtExpirationMinutes = Environment.GetEnvironmentVariable("JWT_EXPIRATION_MINUTES");

            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, usuario.Nome));
            claims.Add(new Claim(ClaimTypes.Email, usuario.Email));

            if (usuario.IsAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "Usuario"));
            }

            SymmetricSecurityKey chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey));

            SigningCredentials credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtExpirationMinutes)),
                signingCredentials: credenciais
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
    }
}