using LojaEstoque.Aplicacao.Dtos;
using LojaEstoque.Dominio.Entidades;

namespace LojaEstoque.Aplicacao.Interfaces
{
    public interface IAplicUsuario
    {
        public Task<Usuario> Cadastrar(UsuarioDto usuarioDto);
        public Task<List<Usuario>> Listar();
        public Task<Usuario> BuscarPorId(Guid id);
        public Task<Usuario> Remover(Guid id);
        public Task<Usuario> Editar(Guid id, UsuarioEditarDto usuarioEditarDto);
        public Task<Usuario> TornarAdmin(Guid id);
    }
}
