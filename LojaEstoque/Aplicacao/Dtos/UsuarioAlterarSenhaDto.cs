using System.Text.Json.Serialization;

namespace LojaEstoque.Aplicacao.Dtos
{
    public class UsuarioAlterarSenhaDto
    {
        [JsonPropertyName("SenhaAtual")]
        public string SenhaAtual { get; set; }

        [JsonPropertyName("NovaSenha")]
        public string NovaSenha { get; set; }
    }
}
