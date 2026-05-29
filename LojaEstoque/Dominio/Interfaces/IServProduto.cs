using LojaEstoque.Dominio.Entidades;
using LojaEstoque.Aplicacao.Interfaces;
using LojaEstoque.Aplicacao.Dtos;

namespace LojaEstoque.Dominio.Interfaces;

public interface IServProduto
{
    Task<Produto?> Cadastrar(ProdutoDto produtoDto);
    Task<List<Produto>> Listar();
    Task<Produto> Remover(Guid id);
    Task<Produto> BuscarPorId(Guid id);
    Task<Produto> Editar(Guid id, ProdutoDto produtoDto);

}
