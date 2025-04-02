using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManager.API.Dto.Pedido;
using StoreManager.API.Models;
using StoreManager.API.Services.Clientes;
using StoreManager.API.Services.Pedidos;

namespace StoreManager.API.Controllers
{
    [Route("api/pedido")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoInterface _pedidoInterface;
        public PedidosController(IPedidoInterface pedidoInterface)
        {
            _pedidoInterface = pedidoInterface;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<PedidoModel>>>> BuscarTodos()
        {
            var listaPedidos = await _pedidoInterface.ListarPedidos();
            return Ok(listaPedidos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<List<PedidoModel>>>> BuscarPorId(int id)
        {
            var pedido = await _pedidoInterface.BuscarPorId(id);
            return Ok(pedido);
        }

        [HttpGet("cliente/{id}")]
        public async Task<ActionResult<ResponseModel<List<PedidoModel>>>> BuscarPorIdCliente(int id)
        {
            var pedido = await _pedidoInterface.BuscarPorIdCliente(id);
            return Ok(pedido);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel<List<PedidoModel>>>> Criar(PedidoCriarDto pedidoCriarDto)
        {
            var pedido = await _pedidoInterface.Criar(pedidoCriarDto);
            return Ok(pedido);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseModel<List<PedidoModel>>>> Excluir(int id)
        {
            var pedido = await _pedidoInterface.Excluir(id);
            return Ok(pedido);
        }

    }
}
