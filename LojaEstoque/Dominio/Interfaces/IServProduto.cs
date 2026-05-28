using LojaEstoque.Dominio.Entidades;
using LojaEstoque.Aplicacao.Interfaces;
using LojaEstoque.Aplicacao.Dtos;

namespace LojaEstoque.Dominio.Interfaces;

public interface IServProduto
{
    Task<Produto?> Cadastrar(ProdutoDto produtoDto);
}
