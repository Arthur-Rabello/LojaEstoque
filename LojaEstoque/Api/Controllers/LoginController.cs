using LojaEstoque.Aplicacao.Dtos;
using LojaEstoque.Aplicacao.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaEstoque.Api.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IAplicLogin _aplicLogin;

        #region LoginController
        public LoginController (IAplicLogin aplicLogin)
        {
            _aplicLogin = aplicLogin;
        }
        #endregion

        #region Login
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto LoginDto)
        {
            var token = await _aplicLogin.Login(LoginDto);
            return Ok(token);
        }
        #endregion
    }
}
