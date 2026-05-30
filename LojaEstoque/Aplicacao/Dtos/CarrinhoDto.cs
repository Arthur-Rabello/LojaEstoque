using System.Text.Json.Serialization;

namespace LojaEstoque.Aplicacao.Dtos
{
    public class CarrinhoDto
    {

        [JsonPropertyName("ProdutoId")]
        public Guid ProdutoId { get; set; }

        [JsonPropertyName("Quantidade")]
        public int Quantidade { get; set; }
    }
}
