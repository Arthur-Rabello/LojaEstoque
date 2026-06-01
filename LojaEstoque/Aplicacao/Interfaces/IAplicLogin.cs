using LojaEstoque.Aplicacao.Dtos;

namespace LojaEstoque.Aplicacao.Interfaces
{
    public interface IAplicLogin
    {
        public Task<LoginRespostaDto> Login(LoginDto loginDto);
    }
}
