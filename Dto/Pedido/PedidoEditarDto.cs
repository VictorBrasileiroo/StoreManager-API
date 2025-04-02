using StoreManager.API.Models;

namespace StoreManager.API.Dto.Pedido
{
    public class PedidoEditarDto
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public decimal Total { get; set; }
    }
}
