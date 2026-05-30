using LojaEstoque.Dominio.Entidades;
using LojaEstoque.Repositories.Contexto;
using LojaEstoque.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LojaEstoque.Repositories.Reps
{
    public class RepProduto : IRepProduto
    {
        private readonly LojaContext _lojaContext;
     
        public RepProduto(LojaContext lojaContext)
        {
            _lojaContext = lojaContext;
        }
        
        #region Cadastrar
        public async Task<Produto?> Cadastrar(Produto produto)
        {
            await _lojaContext.Produtos.AddAsync(produto);
            await _lojaContext.SaveChangesAsync();

            return produto;
        }
        #endregion

        #region Listar
        public async Task<List<Produto>> Listar()
        {
            return await _lojaContext.Produtos
                .AsNoTracking()
                .ToListAsync();
        }
        #endregion

        #region Remover
        public async Task<Produto> Remover(Guid id)
        {   
            Produto? produto = await BuscarPorId(id);

            _lojaContext.Produtos.Remove(produto);

            await _lojaContext.SaveChangesAsync();

            return produto;
        }
        #endregion

        #region BuscarPorId
        public async Task<Produto> BuscarPorId(Guid id)
        {
            Produto? produto = await _lojaContext.Produtos
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return produto;
        }
        #endregion

        #region Editar
        public async Task<Produto> Editar(Produto produto)
        {
            _lojaContext.Produtos.Update(produto);

            await _lojaContext.SaveChangesAsync();

            return produto;
        }
        #endregion

        #region ExisteDescricao
        public async Task<bool> ExisteDescricao(string descricao)
        {
            descricao = descricao.Trim().ToLower();

            return await _lojaContext.Produtos
                .AsNoTracking()
                .AnyAsync(x => x.Descricao.ToLower() == descricao.ToLower());
        }
        #endregion

        #region ExisteDescricaoOutroProduto
        public async Task<bool> ExisteDescricaoOutroProduto(Guid id, string descricao)
        {
            descricao = descricao.Trim().ToLower();
            return await _lojaContext.Produtos
                .AsNoTracking()
                .AnyAsync(x => x.Descricao.ToLower() == descricao.ToLower() && x.Id != id);
        }
        #endregion
    }
}