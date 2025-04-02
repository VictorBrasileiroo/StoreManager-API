namespace StoreManager.API.Models
{
    public class ItemPedidoModel
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public PedidoModel Pedido { get; set; }
        public int ProdutoId { get; set; }
        public ProdutoModel Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal SubTotal { get; set; }

    }
}
