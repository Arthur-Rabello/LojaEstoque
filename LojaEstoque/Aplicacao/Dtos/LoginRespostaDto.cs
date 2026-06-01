namespace LojaEstoque.Aplicacao.Dtos
{
    public class LoginRespostaDto
    {
        public Guid UsuarioId{ get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public string Token { get; set; }
    }
}
