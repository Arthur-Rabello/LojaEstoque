using FluentValidation;
using LojaEstoque.Aplicacao.Dtos;
using LojaEstoque.Aplicacao.Interfaces;
using LojaEstoque.Aplicacao.Mappers;
using LojaEstoque.Aplicacao.Validators;
using LojaEstoque.Dominio.Entidades;
using LojaEstoque.Dominio.Interfaces;

namespace LojaEstoque.Aplicacao.Aplic
{
    public class AplicUsuario : IAplicUsuario
    {
        private readonly IServUsuario _servUsuario;
        private readonly UsuarioValidator _usuarioValidator;
        private readonly UsuarioEditarValidator _usuarioEditarValidator;

        public AplicUsuario(IServUsuario servUsuario, UsuarioValidator usuariovalidator, UsuarioEditarValidator usuarioEditarValidator)
        {
            _servUsuario = servUsuario;
            _usuarioValidator = usuariovalidator;
            _usuarioEditarValidator = usuarioEditarValidator;
        }

        #region Cadastrar
        public async Task<UsuarioRespostaDto?> Cadastrar(UsuarioDto usuarioDto)
        {
            await _usuarioValidator.ValidateAndThrowAsync(usuarioDto);
            Usuario usuario = await _servUsuario.Cadastrar(usuarioDto);
            return UsuarioMapper.ParaRespostaDto(usuario);
        }
        #endregion

        #region Listar
        public async Task<List<UsuarioRespostaDto>> Listar()
        {
            List<Usuario> usuarios = await _servUsuario.Listar();

            return UsuarioMapper.ParaRespostaDtoLista(usuarios);
        }
        #endregion

        #region BuscarPorId
        public async Task<UsuarioRespostaDto> BuscarPorId(Guid id)
        {
            Usuario usuario = await _servUsuario.BuscarPorId(id);

            return UsuarioMapper.ParaRespostaDto(usuario);
        }
        #endregion

        #region Remover
        public async Task Remover(Guid id)
        {
            await _servUsuario.Remover(id);
        }
        #endregion

        #region Editar
        public async Task<UsuarioRespostaDto> Editar(Guid id, UsuarioEditarDto usuarioEditarDto)
        {
            Usuario usuario = await _servUsuario.Editar(id, usuarioEditarDto);

            return UsuarioMapper.ParaRespostaDto(usuario);
        }
        #endregion

        #region TornarAdmin
        public async Task<UsuarioRespostaDto> TornarAdmin(Guid id)
        {
            Usuario usuario = await _servUsuario.TornarAdmin(id);

            return UsuarioMapper.ParaRespostaDto(usuario);

        }
        #endregion
    }
}
