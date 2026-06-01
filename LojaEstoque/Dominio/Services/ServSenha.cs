using LojaEstoque.Dominio.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LojaEstoque.Dominio.Services
{
    public class ServSenha : IServSenha
    {
        private readonly PasswordHasher<object> _passwordHasher;

        public ServSenha()
        {
            _passwordHasher = new PasswordHasher<object>();
        }

        #region GerarHash
        public string GerarHash(string senha)
        {
            string senhaHash = _passwordHasher.HashPassword(null, senha);

            return senhaHash;
        }
        #endregion

        #region VerificarSenha
        public bool VerificarSenha(string senha, string senhaHash)
        {
            PasswordVerificationResult resultado = _passwordHasher.VerifyHashedPassword(null, senhaHash, senha);

            if (resultado == PasswordVerificationResult.Success)
            {
                return true;
            }

            return false;
        }
        #endregion
    }
}
