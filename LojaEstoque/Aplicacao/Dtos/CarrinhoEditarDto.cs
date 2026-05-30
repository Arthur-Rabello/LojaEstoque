using System.Text.Json.Serialization;

namespace LojaEstoque.Aplicacao.Dtos
{
    public class CarrinhoEditarDto
    {
        [JsonPropertyName("Quantidade")]
        public int Quantidade { get; set; }
    }
}
