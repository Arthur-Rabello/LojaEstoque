using LojaEstoque.Dominio.Entidades;
using System.Security.Cryptography.X509Certificates;
using LojaEstoque.Dominio.Interfaces;
using LojaEstoque.Aplicacao.Dtos;

namespace LojaEstoque.Aplicacao.Interfaces;

public interface IAplicProduto {
    public Task<Produto?> Cadastrar(ProdutoDto produtoDto);
}
