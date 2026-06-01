using FluentValidation;
using LojaEstoque.Aplicacao.Dtos;
using LojaEstoque.Aplicacao.Interfaces;
using LojaEstoque.Aplicacao.Validators;
using LojaEstoque.Dominio.Entidades;
using LojaEstoque.Dominio.Interfaces;

namespace LojaEstoque.Aplicacao.Aplic
{
    public class AplicUsuario : IAplicUsuario
    {
        private readonly IServUsuario _servUsuario;
        private readonly UsuarioValidator _usuarioValidator;
        private readonly LoginValidator _loginValidator;
        private readonly UsuarioEditarValidator _usuarioEditarValidator;

        public AplicUsuario(IServUsuario servUsuario, UsuarioValidator usuariovalidator, LoginValidator loginValidator, UsuarioEditarValidator usuarioEditarValidator)
        {
            _servUsuario = servUsuario;
            _usuarioValidator = usuariovalidator;
            _loginValidator = loginValidator;
            _usuarioEditarValidator = usuarioEditarValidator;
        }

        #region Cadastrar
        public async Task<Usuario?> Cadastrar(UsuarioDto usuarioDto)
        {
            await _usuarioValidator.ValidateAndThrowAsync(usuarioDto);
            Usuario usuario = await _servUsuario.Cadastrar(usuarioDto);
            return usuario;
        }
        #endregion

        #region Listar
        public async Task<List<Usuario>> Listar()
        {
            return await _servUsuario.Listar();
        }
        #endregion

        #region BuscarPorId
        public async Task<Usuario> BuscarPorId(Guid id)
        {
            return await _servUsuario.BuscarPorId(id);
        }
        #endregion

        #region Remover
        public async Task<Usuario> Remover(Guid id)
        {
            return await _servUsuario.Remover(id);
        }
        #endregion

        #region Editar
        public async Task<Usuario> Editar(Guid id, UsuarioEditarDto usuarioEditarDto)
        {
            await _usuarioEditarValidator.ValidateAndThrowAsync(usuarioEditarDto);
            return await _servUsuario.Editar(id, usuarioEditarDto);
        }
        #endregion

        #region TornarAdmin
        public async Task<Usuario> TornarAdmin(Guid id)
        {
            return await _servUsuario.TornarAdmin(id);
        }
        #endregion
    }
}
