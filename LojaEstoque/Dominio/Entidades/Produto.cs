namespace LojaEstoque.Dominio.Models
{
    public class Produto
    {
        public Guid Id { get; set; }
        public string? Descricao { get; set; }
        public float PrecoUnitario { get; set; }
        public int Quantidade { get; set; }
    }
}
