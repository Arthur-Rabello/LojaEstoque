using LojaEstoque.Dominio.Entidades;
using System.Security.Cryptography.X509Certificates;
using LojaEstoque.Dominio.Interfaces;
using LojaEstoque.Aplicacao.Dtos;

namespace LojaEstoque.Aplicacao.Interfaces;

public interface IAplicProduto {
    public Task<Produto?> Cadastrar(ProdutoDto produtoDto);
    public Task<List<Produto?>> Listar();
    public Task<Produto?> Remover(Guid id);
    public Task<Produto?> BuscarPorId(Guid id);
    public Task<Produto?> Editar(Guid id, ProdutoDto produtoDto);
}
