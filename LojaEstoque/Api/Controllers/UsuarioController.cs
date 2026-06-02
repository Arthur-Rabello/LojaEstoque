using LojaEstoque.Aplicacao.Dtos;
using LojaEstoque.Aplicacao.Interfaces;
using LojaEstoque.Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LojaEstoque.Api.Helpers;

namespace LojaEstoque.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
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
            UsuarioRespostaDto usuario = await _aplicUsuario.Cadastrar(usuarioDto);

            return Ok(usuario);
        }
        #endregion

        #region Listar
        [Authorize(Roles = "Admin")]
        [HttpGet("Listar")]
        public async Task<IActionResult> Listar()
        {
            List<UsuarioRespostaDto> usuarios = await _aplicUsuario.Listar();

            return Ok(usuarios);
        }
        #endregion

        #region BuscarPorId
        [Authorize]
        [HttpGet("BuscarPorId/{id}")]
        public async Task<IActionResult> BuscarPorId(Guid id)
        {
            if (!UsuarioAutenticadoHelper.UsuarioPodeAcessar(User, id))
            {
                return Forbid();
            }


            UsuarioRespostaDto usuario = await _aplicUsuario.BuscarPorId(id);

            return Ok(usuario);
        }
        #endregion

        #region Remover
        [Authorize]
        [HttpDelete("Remover/{id}")]
        public async Task<IActionResult> Remover(Guid id)
        {
            if (!UsuarioAutenticadoHelper.UsuarioPodeAcessar(User, id))
            {
                return Forbid();
            }

            await _aplicUsuario.Remover(id);

            return NoContent();
        }
        #endregion

        #region Editar
        [Authorize]
        [HttpPut("Editar/{id}")]
        public async Task<IActionResult> Editar(Guid id, [FromBody] UsuarioEditarDto usuarioEditarDto)
        {
            if (!UsuarioAutenticadoHelper.UsuarioPodeAcessar(User, id))
            {
                return Forbid();
            }
            UsuarioRespostaDto usuario = await _aplicUsuario.Editar(id, usuarioEditarDto);

            return Ok(usuario);
        }
        #endregion

        #region TornarAdmin
        [Authorize(Roles = "Admin")]
        [HttpPut("tornar-admin/{id}")]
        public async Task<IActionResult> TornarAdmin(Guid id)
        {
            UsuarioRespostaDto usuario = await _aplicUsuario.TornarAdmin(id);

            return Ok(usuario);
        }
        #endregion

    }
}
