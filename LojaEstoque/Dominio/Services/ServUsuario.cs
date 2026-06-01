using LojaEstoque.Aplicacao.Dtos;
using LojaEstoque.Dominio.Entidades;
using LojaEstoque.Dominio.Exceptions;
using LojaEstoque.Dominio.Interfaces;
using LojaEstoque.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace LojaEstoque.Dominio.Services
{
    public class ServUsuario : IServUsuario
    {
        private readonly IRepUsuario _repUsuario;
        private readonly IServToken _servToken;
        private readonly IServSenha _servSenha;

        #region ServUsuario
        public ServUsuario(IRepUsuario repUsuario, IServToken servToken, IServSenha servSenha)
        {
            _repUsuario = repUsuario;
            _servToken = servToken;
            _servSenha = servSenha;
        }
        #endregion

        #region Cadastrar
        public async Task<Usuario?> Cadastrar(UsuarioDto usuarioDto)
        {

            string emailnormalizado = usuarioDto.Email?.Trim().ToLowerInvariant();
            bool emailexiste = await _repUsuario.ExistePorEmail(emailnormalizado);

            if (emailexiste)
            {
                throw new RegraDeNegocioException("Já existe um usuário com este email cadastrado");
            }

            Usuario? usuario = new Usuario();

            usuario.Nome = usuarioDto.Nome;
            usuario.Email = emailnormalizado;
            usuario.SenhaHash = _servSenha.GerarHash(usuarioDto.Senha);
            usuario.IsAdmin = false;

            Usuario usuariocadastrado = await _repUsuario.Cadastrar(usuario);

            return usuariocadastrado;

        }
        #endregion

        #region Listar
        public async Task<List<Usuario>> Listar()
        {
            return await _repUsuario.Listar();
        }
        #endregion

        #region BuscarPorId
        public async Task<Usuario> BuscarPorId(Guid id)
        {
            return await _repUsuario.BuscarPorId(id);
        }
        #endregion

        #region Remover
        public async Task<Usuario> Remover(Guid id)
        {
            Usuario usuario = await _repUsuario.BuscarPorId(id);
            return await _repUsuario.Remover(id);
        }
        #endregion

        #region Editar
        public async Task<Usuario> Editar(Guid id, UsuarioEditarDto usuarioEditarDto)
        {
            Usuario usuario = await _repUsuario.BuscarPorId(id);
            if (usuario == null)
            {
                throw new RegraDeNegocioException("Usuário não encontrado.");
            }
            if (usuario.Email != usuarioEditarDto.Email && await _repUsuario.ExistePorEmail(usuarioEditarDto.Email))
            {
                throw new RegraDeNegocioException("Email já cadastrado.");
            }
            usuario.Nome = usuarioEditarDto.Nome;
            usuario.Email = usuarioEditarDto.Email;

            Usuario usuarioeditado = await _repUsuario.Editar(usuario);

            return usuarioeditado;
        }
        #endregion

        #region TornarAdmin
        public async Task<Usuario> TornarAdmin(Guid id)
        {
            Usuario usuario = await _repUsuario.BuscarPorId(id);
            if (usuario == null)
            {
                throw new RegraDeNegocioException("Usuário não encontrado.");
            }
            usuario.IsAdmin = true;
            Usuario usuarioeditado = await _repUsuario.Editar(usuario);
            return usuarioeditado;
        }

        #endregion

        #region AlterarSenha
        public async Task<Usuario> AlterarSenha(Guid id, UsuarioAlterarSenhaDto usuarioAlterarSenhaDto)
        {
            Usuario? usuario = await _repUsuario.BuscarPorId(id);

            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            bool senhaAtualValida = _servSenha.VerificarSenha(usuarioAlterarSenhaDto.SenhaAtual, usuario.SenhaHash);

            if (senhaAtualValida == false)
            {
                throw new Exception("Senha atual inválida.");
            }

            usuario.SenhaHash = _servSenha.GerarHash(usuarioAlterarSenhaDto.NovaSenha);

            Usuario usuarioEditado = await _repUsuario.Editar(usuario);

            return usuarioEditado;
        }
        #endregion
    }
}
