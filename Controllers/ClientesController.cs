using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManager.API.Dto.Cliente;
using StoreManager.API.Models;
using StoreManager.API.Services.Clientes;

namespace StoreManager.API.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteInterface _clienteInterface;
        public ClientesController(IClienteInterface clienteInterface)
        {
            _clienteInterface = clienteInterface;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<ClientModel>>>> BuscarTodos()
        {
            var listaClientes = await _clienteInterface.ListarClientes();
            return Ok(listaClientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<ClientModel>>> BuscarPorId(int id)
        {
            var cliente = await _clienteInterface.BuscarClientePorId(id);
            return Ok(cliente);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseModel<List<ClientModel>>>> Excluir(int id)
        {
            var cliente = await _clienteInterface.ExcluirCliente(id);
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel<List<ClientModel>>>> Criar(ClienteCriarDto clienteCriarDto)
        {
            var newCliente = await _clienteInterface.AdicionarCliente(clienteCriarDto);
            return Ok(newCliente);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseModel<List<ClientModel>>>> Atualizar(int id,ClienteEditarDto clienteEditarDto)
        {
            if (id != clienteEditarDto.Id)
            {
                return BadRequest("O ID na URL não corresponde ao ID no objeto");
            }

            var cliente = await _clienteInterface.EditarCliente(clienteEditarDto);
            return Ok(cliente);
        }
    }
}
