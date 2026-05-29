using LojaEstoque.Dominio.Entidades;
using LojaEstoque.Repositories.Contexto;
using LojaEstoque.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LojaEstoque.Repositories.Reps
{
    public class RepCarrinho : IRepCarrinho
    {
        private readonly LojaContext _lojaContext;

        public RepCarrinho(LojaContext lojaContext)
        {
            _lojaContext = lojaContext;
        }

        #region Cadastrar
        public async Task<Carrinho?> Cadastrar(Carrinho carrinho)
            {
            await _lojaContext.Carrinho.AddAsync(carrinho);
            await _lojaContext.SaveChangesAsync();
            return carrinho;
        }
        #endregion

        #region Listar
        public async Task<List<Carrinho?>> Listar()
        {
            return await _lojaContext.Carrinho.ToListAsync();
        }
        #endregion

        #region BuscarPorId
        public async Task<Carrinho> BuscarPorId(Guid id)
        {
            Carrinho? carrinho = await _lojaContext.Carrinho
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return carrinho;
        }
        #endregion

        #region Remover
        public async Task<Carrinho> Remover(Guid id)
        {
            Carrinho? carrinho = await BuscarPorId(id);
            _lojaContext.Carrinho.Remove(carrinho);
            await _lojaContext.SaveChangesAsync();
            return carrinho;
        }
        #endregion

        #region Editar
        public async Task<Carrinho> Editar(Carrinho carrinho)
        {
            _lojaContext.Carrinho.Update(carrinho);
            await _lojaContext.SaveChangesAsync();
            return carrinho;
        }
        #endregion
    }
}
