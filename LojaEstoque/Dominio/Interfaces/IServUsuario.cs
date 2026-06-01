using LojaEstoque.Aplicacao.Dtos;
using LojaEstoque.Dominio.Entidades;

namespace LojaEstoque.Dominio.Interfaces
{
    public interface IServUsuario
    {
        public Task<Usuario?> Cadastrar(UsuarioDto usuarioDto);
        public Task<List<Usuario?>> Listar();
        public Task<Usuario?> BuscarPorId(Guid id);
        public Task<Usuario?> Remover(Guid id);
        public Task<Usuario?> Editar(Guid Id, UsuarioEditarDto usuarioEditarDto);
        public Task<Usuario?> TornarAdmin(Guid id);
    }
}
