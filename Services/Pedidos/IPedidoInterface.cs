using StoreManager.API.Dto.Pedido;
using StoreManager.API.Models;

namespace StoreManager.API.Services.Pedidos
{
    public interface IPedidoInterface
    {
        Task<ResponseModel<List<PedidoModel>>> ListarPedidos();
        Task<ResponseModel<PedidoModel>> BuscarPorId(int id);
        Task<ResponseModel<PedidoModel>> Criar(PedidoCriarDto pedidoCriarDto);
        Task<ResponseModel<List<PedidoModel>>> Excluir(int id);
        Task<ResponseModel<List<PedidoModel>>> BuscarPorIdCliente(int idCliente);

    }
}
