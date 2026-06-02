namespace LojaEstoque.Aplicacao.Dtos
{
    public class UsuarioRespostaDto
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}