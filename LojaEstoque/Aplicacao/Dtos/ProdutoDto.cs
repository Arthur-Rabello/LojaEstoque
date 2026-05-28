using System.Text.Json.Serialization;


namespace LojaEstoque.Aplicacao.Dtos;

public class ProdutoDto
{
	[JsonPropertyName("Descricao")]
	public string? Descricao { get; set; }

	[JsonPropertyName("PrecoUnitario")]
	public decimal PrecoUnitario { get; set; }
	
	[JsonPropertyName("Quantidade")]
	public int Quantidade { get; set; }
}
