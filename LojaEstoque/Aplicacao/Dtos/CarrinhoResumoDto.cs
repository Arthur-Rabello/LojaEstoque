using LojaEstoque.Dominio.Entidades;
using System.Text.Json.Serialization;

namespace LojaEstoque.Aplicacao.Dtos
{
    public class CarrinhoResumoDto
    {
        [JsonPropertyName("Itens")]
        public List<Carrinho> Itens { get; set; }

        [JsonPropertyName("PrecoTotal")]
        public decimal PrecoTotal { get; set; }
    }
}
