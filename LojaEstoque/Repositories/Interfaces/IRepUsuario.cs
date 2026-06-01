using LojaEstoque.Dominio.Entidades;

namespace LojaEstoque.Repositories.Interfaces
{
    public interface IRepUsuario
    {
        public Task<Usuario?> Cadastrar(Usuario usuario);
        public Task<List<Usuario?>> Listar();
        public Task<Usuario?> BuscarPorId(Guid id);
        public Task<Usuario?> Remover(Guid id);
        public Task<Usuario?> Editar(Usuario usuario);
        public Task<bool> ExistePorEmail(string email);

        public Task<Usuario?> BuscarPorEmail(string email);
    }
}
