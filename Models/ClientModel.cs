using System.Text.Json.Serialization;

namespace StoreManager.API.Models
{
    public class ClientModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }

        [JsonIgnore]
        public ICollection<PedidoModel> Pedidos { get; set; }
    }
}
