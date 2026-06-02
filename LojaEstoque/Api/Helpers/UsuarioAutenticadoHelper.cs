using System.Security.Claims;

namespace LojaEstoque.Api.Helpers
{
    public static class UsuarioAutenticadoHelper
    {
        #region ObterUsuarioId
        public static Guid? ObterUsuarioId(ClaimsPrincipal user)
        {
            string? usuarioIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(usuarioIdClaim))
            {
                return null;
            }

            bool idValido = Guid.TryParse(usuarioIdClaim, out Guid usuarioId);

            if (!idValido)
            {
                return null;
            }

            return usuarioId;
        }
        #endregion

        #region UsuarioPodeAcessar
        public static bool UsuarioPodeAcessar(ClaimsPrincipal user, Guid id)
        {
            bool isAdmin = user.IsInRole("Admin");

            Guid? usuarioIdLogado = ObterUsuarioId(user);

            if (usuarioIdLogado == null)
            {
                return false;
            }

            return isAdmin || usuarioIdLogado == id;
        }
        #endregion
    }
}