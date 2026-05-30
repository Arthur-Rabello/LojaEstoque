namespace LojaEstoque.Dominio.Entidades
{
    public class Carrinho
    {
        public Guid Id { get; set; }
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal PrecoTotal { get; set; }

    }
}
