using LojaEstoque.Aplicacao.Dtos;
using LojaEstoque.Dominio.Entidades;
using LojaEstoque.Repositories.Contexto;
using LojaEstoque.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LojaEstoque.Repositories.Reps
{
    public class RepUsuario : IRepUsuario
    {
        private readonly LojaContext _lojaContext;

        public RepUsuario(LojaContext lojaContext)
        {
            _lojaContext = lojaContext;
        }

        #region Cadastrar
        public async Task<Usuario?> Cadastrar(Usuario usuario)
        {
            await _lojaContext.Usuario.AddAsync(usuario);
            await _lojaContext.SaveChangesAsync();
            return usuario;
        }
        #endregion

        #region Listar
        public async Task<List<Usuario?>> Listar()
        {
            return await _lojaContext.Usuario
                .AsNoTracking()
                .ToListAsync();
        }
        #endregion

        #region BuscarPorId
        public async Task<Usuario?> BuscarPorId(Guid id)
        {
            Usuario? Usuario = await _lojaContext.Usuario
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(u => u.Id == id);
            return Usuario;
        }
        #endregion

        #region Remover
        public async Task Remover(Usuario usuario)
        {
            _lojaContext.Usuario.Remove(usuario);

            await _lojaContext.SaveChangesAsync();

        }
        #endregion

        #region Editar
        public async Task<Usuario?> Editar(Usuario usuario)
        {
            _lojaContext.Usuario.Update(usuario);
            await _lojaContext.SaveChangesAsync();
            return usuario;
        }
        #endregion

        #region ExistePorEmail
        public async Task<bool> ExistePorEmail(string email)
        {
            bool existe = await _lojaContext.Usuario
                .AsNoTracking()
                .AnyAsync(u => u.Email == email);

            return existe;
        }
        #endregion

        #region BuscarPorEmailESenha
        public async Task<Usuario?> BuscarPorEmail(string email)
        {
            Usuario? usuario = await _lojaContext.Usuario
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
            return usuario;
        }
        #endregion
    }
}
