using System.Text.Json.Serialization;

namespace StoreManager.API.Models
{
    public class PedidoModel
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public ClientModel Cliente { get; set; }
        public decimal Total { get; set; }

        [JsonIgnore]
        public ICollection<ItemPedidoModel> Itens { get; set; }
    }
}
