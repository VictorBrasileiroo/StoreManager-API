using StoreManager.API.Dto.Cliente;
using StoreManager.API.Models;

namespace StoreManager.API.Services.Clientes
{
    public interface IClienteInterface
    {
        //READ
        Task<ResponseModel<List<ClientModel>>> ListarClientes();
        Task<ResponseModel<ClientModel>> BuscarClientePorId(int idCliente);

        //DELETE
        Task<ResponseModel<List<ClientModel>>> ExcluirCliente(int idCliente);

        //CREATE
        Task<ResponseModel<List<ClientModel>>> AdicionarCliente(ClienteCriarDto clienteCriarDto);

        //UPDATE
        Task<ResponseModel<List<ClientModel>>> EditarCliente(ClienteEditarDto clienteEditarDto);
    }
}
