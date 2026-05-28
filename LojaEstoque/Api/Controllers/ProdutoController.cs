using LojaEstoque.Aplicacao.Dtos;
using LojaEstoque.Aplicacao.Interfaces;
using LojaEstoque.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace LojaEstoque.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProdutoController : Controller
    {
        private readonly IAplicProduto _aplicProduto;
        #region ProdutoController
        public ProdutoController(IAplicProduto aplicProduto)
        {
            _aplicProduto = aplicProduto;
        }
        #endregion

        #region CadastrarProduto
        [HttpPost("Cadastrar")]
        public async Task<IActionResult> Cadastar([FromBody] ProdutoDto produtoDto)
        {
            Produto produto = await _aplicProduto.Cadastrar(produtoDto);
            return Ok(produto);

        }
        #endregion
    }
}
