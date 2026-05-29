using LojaEstoque.Aplicacao.Dtos;
using LojaEstoque.Aplicacao.Interfaces;
using LojaEstoque.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace LojaEstoque.Api.Controllers
{
    [Route("api/[controller]")]
    public class CarrinhoController : Controller
    {
        private readonly IAplicCarrinho _aplicCarrinho;

        #region CarrinhoController
        public CarrinhoController(IAplicCarrinho aplicCarrinho)
        {
            _aplicCarrinho = aplicCarrinho;
        }
        #endregion

        #region Cadastrar
        [HttpPost("Cadastrar")]
        public async Task<IActionResult> Cadastar([FromBody] CarrinhoDto carrinhoDto)
        {
            Carrinho carrinho = await _aplicCarrinho.Cadastrar(carrinhoDto);
            return Ok(carrinho);
        }
        #endregion

        #region Listar
        [HttpGet("Listar")]
        public async Task<IActionResult> Listar()
        {
            List<Carrinho?> carrinhos = await _aplicCarrinho.Listar();
            return Ok(carrinhos);
        }
        #endregion

        #region BuscarPorId
        [HttpGet("BuscarPorId/{id}")]
        public async Task<IActionResult> BuscarPorId(Guid id)
        {
            Carrinho carrinho = await _aplicCarrinho.BuscarPorId(id);
            return Ok(carrinho);
        }
        #endregion

        #region Remover
        [HttpDelete("Remover/{id}")]
        public async Task<IActionResult> Remover(Guid id)
        {
            Carrinho carrinho = await _aplicCarrinho.Remover(id);
            return Ok(carrinho);
        }
        #endregion

        #region Editar
        [HttpPut("Editar/{id}")]
        public async Task<IActionResult> Editar(Guid id, [FromBody] CarrinhoDto carrinhoDto)
        {
            Carrinho carrinho = await _aplicCarrinho.Editar(id, carrinhoDto);
            return Ok(carrinho);
        }
        #endregion

    }
}
