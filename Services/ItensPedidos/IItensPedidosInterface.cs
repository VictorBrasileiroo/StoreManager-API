using StoreManager.API.Dto.ItemPedido;
using StoreManager.API.Dto.Pedido;
using StoreManager.API.Models;

namespace StoreManager.API.Services.ItensPedidos
{
    public interface IItensPedidosInterface
    {
        Task<ResponseModel<List<ItemPedidoModel>>> BuscarPorPedido(int idPedido);
        Task<ResponseModel<List<ItemPedidoModel>>> ListarItens();
        Task<ResponseModel<List<ItemPedidoModel>>> Editar(ItemPedidoEditarDto itemPedidoEditarDto);
        Task<ResponseModel<List<ItemPedidoModel>>> Criar(ItemPedidoCriarDto itemPedidoCriarDto);
        Task<ResponseModel<List<ItemPedidoModel>>> Excluir(int id);
        Task<ResponseModel<ItemPedidoModel>> BuscarPorId(int id);
        
    }
}
