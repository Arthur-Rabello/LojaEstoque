using LojaEstoque.Aplicacao.Dtos;

namespace LojaEstoque.Dominio.Interfaces
{
    public interface IServLogin
    {
        public Task<LoginRespostaDto> Login(LoginDto loginDto);
    }
}
