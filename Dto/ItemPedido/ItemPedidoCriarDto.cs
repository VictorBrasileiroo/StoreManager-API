using StoreManager.API.Models;

namespace StoreManager.API.Dto.ItemPedido
{
    public class ItemPedidoCriarDto
    {
        public int PedidoId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}
