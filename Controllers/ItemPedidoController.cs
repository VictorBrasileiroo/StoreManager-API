using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManager.API.Dto.ItemPedido;
using StoreManager.API.Models;
using StoreManager.API.Services.Clientes;
using StoreManager.API.Services.ItensPedidos;

namespace StoreManager.API.Controllers
{
    [Route("api/itens")]
    [ApiController]
    public class ItemPedidoController : ControllerBase
    {
        private readonly IItensPedidosInterface _itensPedidosInterface;
        public ItemPedidoController(IItensPedidosInterface itensPedidosInterface)
        {
            _itensPedidosInterface = itensPedidosInterface;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<ItemPedidoModel>>>> ListarItens()
        {
            var itens = await _itensPedidosInterface.ListarItens();
            return Ok(itens);
        }

        [HttpGet("pedido/{idPedido}")]
        public async Task<ActionResult<ResponseModel<List<ItemPedidoModel>>>> BuscarPorPedido(int idPedido)
        {
            var itens = await _itensPedidosInterface.BuscarPorPedido(idPedido);
            return Ok(itens);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<ItemPedidoModel>>> BuscarPorId(int id)
        {
            var item = await _itensPedidosInterface.BuscarPorId(id);
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel<ItemPedidoModel>>> Criar(ItemPedidoCriarDto itemPedidoCriarDto)
        {
            var item = await _itensPedidosInterface.Criar(itemPedidoCriarDto);
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseModel<List<ItemPedidoModel>>>> Excluir(int id)
        {
            var item = await _itensPedidosInterface.Excluir(id);
            return Ok(item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseModel<ItemPedidoModel>>> Editar(ItemPedidoEditarDto itemPedidoEditarDto)
        {
            var item = await _itensPedidosInterface.Editar(itemPedidoEditarDto);
            return Ok(item);
        }
    }
}
