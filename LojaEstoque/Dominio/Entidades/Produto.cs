namespace LojaEstoque.Dominio.Entidades
{
    public class Produto
    {
        public Guid Id { get; set; }
        public string? Descricao { get; set; }
        public decimal PrecoUnitario { get; set; }
        public int Quantidade { get; set; }
    }
}
