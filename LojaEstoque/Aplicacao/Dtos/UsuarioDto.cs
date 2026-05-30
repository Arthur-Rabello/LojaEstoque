using System.Text.Json.Serialization;

namespace LojaEstoque.Aplicacao.Dtos
{
    public class UsuarioDto
    {
        [JsonPropertyName("Nome")]
        public string? Nome { get; set; }

        [JsonPropertyName("Email")]
        public string? Email { get; set; }

        [JsonPropertyName("Senha")]
        public string? Senha { get; set; }
    }
}
