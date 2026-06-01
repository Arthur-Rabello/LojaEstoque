using LojaEstoque.Dominio.Entidades;

namespace LojaEstoque.Dominio.Interfaces
{
    public interface IServToken
    {
        string GerarToken(Usuario usuario);
    }
}
