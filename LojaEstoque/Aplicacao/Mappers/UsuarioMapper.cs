using LojaEstoque.Aplicacao.Dtos;
using LojaEstoque.Dominio.Entidades;

namespace LojaEstoque.Aplicacao.Mappers
{
    public static class UsuarioMapper
    {
        #region ParaRespostaDto
        public static UsuarioRespostaDto ParaRespostaDto(Usuario usuario)
        {
            return new UsuarioRespostaDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                IsAdmin = usuario.IsAdmin
            };
        }
        #endregion

        #region ParaRespostaDtoLista
        public static List<UsuarioRespostaDto> ParaRespostaDtoLista(List<Usuario> usuarios)
        {
            return usuarios.Select(usuario => ParaRespostaDto(usuario)).ToList();
        }
        #endregion
    }
}