using LojaEstoque.Dominio.Entidades;
using LojaEstoque.Repositories.Contexto;
using LojaEstoque.Repositories.Interfaces;

namespace LojaEstoque.Repositories.Reps
{
    public class RepProduto : IRepProduto
    {
        private readonly LojaContext _lojaContext;

        #region RepProduto
        public RepProduto(LojaContext lojaContext)
        {
            _lojaContext = lojaContext;
        }
        #endregion

        #region Cadastrar
        public async Task<Produto?> Cadastrar(Produto produto)
        {
            await _lojaContext.Produtos.AddAsync(produto);
            await _lojaContext.SaveChangesAsync();

            return produto;
        }
        #endregion

        //#region Listar
        //public async Task<List<Produto>> Listar()
        //{
        //    return await _lojaContext.Produtos
        //        .AsNoTracking()
        //        .ToListAsync();
        //}
        //#endregion

        //#region BuscarPorId
        //public async Task<Produto> BuscarPorId(long id)
        //{
        //    return await _lojaContext.Produtos
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync(x => x.Id == id);
        //}
        //#endregion

        //#region ExistePorDescricao
        //public async Task<bool> ExistePorDescricao(string descricao)
        //{
        //    return await _lojaContext.Produtos
        //        .AsNoTracking()
        //        .AnyAsync(x => x.Descricao == descricao);
        //}
        //#endregion
    }
}