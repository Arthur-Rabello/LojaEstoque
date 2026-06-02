using LojaEstoque.Aplicacao.Dtos;
using LojaEstoque.Dominio.Entidades;

namespace LojaEstoque.Aplicacao.Interfaces
{
    public interface IAplicUsuario
    {
        public Task<UsuarioRespostaDto> Cadastrar(UsuarioDto usuarioDto);
        public Task<List<UsuarioRespostaDto>> Listar();
        public Task<UsuarioRespostaDto> BuscarPorId(Guid id);
        public Task Remover(Guid id);
        public Task<UsuarioRespostaDto> Editar(Guid id, UsuarioEditarDto usuarioEditarDto);
        public Task<UsuarioRespostaDto> TornarAdmin(Guid id);
    }
}
