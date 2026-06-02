using LojaEstoque.Aplicacao.Dtos;
using LojaEstoque.Aplicacao.Interfaces;
using LojaEstoque.Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LojaEstoque.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IAplicProduto _aplicProduto;
        #region ProdutoController
        public ProdutoController(IAplicProduto aplicProduto)
        {
            _aplicProduto = aplicProduto;
        }
        #endregion

        #region Cadastrar
        [Authorize(Roles = "Admin")]
        [HttpPost("Cadastrar")]
        public async Task<IActionResult> Cadastar([FromBody] ProdutoDto produtoDto)
        {
            Produto produto = await _aplicProduto.Cadastrar(produtoDto);
            return Ok(produto);

        }
        #endregion

        #region Listar
        [HttpGet("Listar")]
        public async Task<IActionResult> Listar()
        {
            List<Produto> produtos = await _aplicProduto.Listar();
            return Ok(produtos);
        }
        #endregion

        #region BuscarPorId
        [HttpGet("BuscarPorId/{id}")]
        public async Task<IActionResult> BuscarPorId(Guid id)
        {
            Produto produto = await _aplicProduto.BuscarPorId(id);
            return Ok(produto);
        }
        #endregion

        #region Remover
        [Authorize(Roles = "Admin")]
        [HttpDelete("Remover/{id}")]
        public async Task<IActionResult> Remover(Guid id)
        {
            Produto produto = await _aplicProduto.Remover(id);
            return Ok(produto);
        }
        #endregion

        #region Editar
        [Authorize(Roles = "Admin")]
        [HttpPut("Editar/{id}")]
        public async Task<IActionResult> Editar(Guid id, [FromBody] ProdutoDto produtoDto)
        {
            Produto produto = await _aplicProduto.Editar(id, produtoDto);
            return Ok(produto);
        }
        #endregion

    }
}
