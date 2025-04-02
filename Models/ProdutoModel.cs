using System.Text.Json.Serialization;

namespace StoreManager.API.Models
{
    public class ProdutoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }

        [JsonIgnore]
        public ICollection<ItemPedidoModel> ItensPedidos { get; set; }
    }
}
