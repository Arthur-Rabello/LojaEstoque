using LojaEstoque.Aplicacao.Aplic;
using LojaEstoque.Aplicacao.Dtos;
using LojaEstoque.Aplicacao.Interfaces;
using LojaEstoque.Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LojaEstoque.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IAplicUsuario _aplicUsuario;

        public UsuarioController(IAplicUsuario aplicUsuario)
        {
            _aplicUsuario = aplicUsuario;
        }

        #region Cadastrar
        [HttpPost("Cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] UsuarioDto usuarioDto)
        {
            var usuario = await _aplicUsuario.Cadastrar(usuarioDto);
            return Ok(usuario);
        }
        #endregion

        #region Listar
        [HttpGet("Listar")]
        public async Task<IActionResult> Listar()
        {
            var usuarios = await _aplicUsuario.Listar();
            return Ok(usuarios);
        }
        #endregion

        #region BuscarPorId
        [HttpGet("BuscarPorId/{id}")]
        public async Task<IActionResult> BuscarPorId(Guid id)
        {
            var usuario = await _aplicUsuario.BuscarPorId(id);
            return Ok(usuario);
        }
        #endregion

        #region Remover
        [HttpDelete("Remover/{id}")]
        public async Task<IActionResult> Remover(Guid id)
        {
            var usuario = await _aplicUsuario.Remover(id);
            return Ok(usuario);
        }
        #endregion

        #region Editar
        [HttpPut("Editar/{id}")]
        public async Task<IActionResult> Editar(Guid id, [FromBody] UsuarioEditarDto usuarioEditarDto)
        {
            var usuario = await _aplicUsuario.Editar(id, usuarioEditarDto);
            return Ok(usuario);
        }
        #endregion

        #region TornarAdmin
        [Authorize(Roles = "Admin")]
        [HttpPut("tornar-admin/{id}")]
        public async Task<IActionResult> TornarAdmin(Guid id)
        {
            Usuario usuario = await _aplicUsuario.TornarAdmin(id);
            return Ok(usuario);
        }
        #endregion

    }
}
