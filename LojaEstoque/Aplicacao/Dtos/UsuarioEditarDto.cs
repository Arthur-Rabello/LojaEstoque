using System.Text.Json.Serialization;

namespace LojaEstoque.Aplicacao.Dtos
{
    public class UsuarioEditarDto
    {
        [JsonPropertyName("Nome")]
        public string? Nome { get; set; }

        [JsonPropertyName("Email")]
        public string? Email { get; set; }

    }
}
