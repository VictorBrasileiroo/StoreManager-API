using Microsoft.EntityFrameworkCore;
using StoreManager.API.Data;
using StoreManager.API.Dto.ItemPedido;
using StoreManager.API.Dto.Pedido;
using StoreManager.API.Models;

namespace StoreManager.API.Services.ItensPedidos
{
    public class ItensPedidosServices : IItensPedidosInterface
    {
        private readonly AppDbContext _context;
        public ItensPedidosServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<ItemPedidoModel>> BuscarPorId(int id)
        {
            ResponseModel<ItemPedidoModel> response = new ResponseModel<ItemPedidoModel>();
            try
            {
                var itens = await _context.ItensPedidos
                    .Include(i => i.Pedido.Cliente)
                    .Include(i => i.Produto)
                    .FirstOrDefaultAsync(i => i.Id == id);

                if (itens == null)
                {
                    response.Mensagem = "Nenhum item cadastrado!";
                    response.Status = false;
                    return response;
                }

                response.Dados = itens;
                response.Mensagem = "Item localizado com sucesso!";
                return response;

            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<ItemPedidoModel>>> BuscarPorPedido(int idPedido)
        {
            ResponseModel<List<ItemPedidoModel>> response = new ResponseModel<List<ItemPedidoModel>>();
            try
            {
                var itens = await _context.ItensPedidos
                    .Where(i => i.PedidoId == idPedido)
                    .Include(i => i.Pedido.Cliente)
                    .Include(i => i.Produto)
                    .ToListAsync();

                if (!itens.Any())
                {
                    response.Mensagem = "Nenhum item cadastrado!";
                    response.Status = false;
                    return response;
                }

                response.Dados = itens;
                response.Mensagem = "Todos os itens listados com sucesso!";
                return response;

            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<ItemPedidoModel>>> Criar(ItemPedidoCriarDto itemPedidoCriarDto)
        {
            ResponseModel<List<ItemPedidoModel>> response = new ResponseModel<List<ItemPedidoModel>>();

            try
            {
                var produto = await _context.Produtos.FindAsync(itemPedidoCriarDto.ProdutoId);
                if (produto == null)
                {
                    response.Mensagem = "Produto não existente";
                    response.Status = false;
                    return response;
                }

                if (itemPedidoCriarDto.Quantidade > produto.Estoque)
                {
                    response.Mensagem = "Quantidade excedente ao estoque disponível";
                    response.Status = false;
                    return response;
                }

                var pedido = await _context.Pedidos.FindAsync(itemPedidoCriarDto.PedidoId);
                if (pedido == null)
                {
                    response.Mensagem = "Pedido não existente";
                    response.Status = false;
                    return response;
                }

                var item = new ItemPedidoModel()
                {
                    PedidoId = itemPedidoCriarDto.PedidoId,
                    ProdutoId = itemPedidoCriarDto.ProdutoId,
                    Quantidade = itemPedidoCriarDto.Quantidade,
                    ValorUnitario = produto.Preco,
                    SubTotal = itemPedidoCriarDto.Quantidade * produto.Preco,
                };

                pedido.Total += item.SubTotal;
                produto.Estoque -= itemPedidoCriarDto.Quantidade;

                _context.ItensPedidos.Add(item);
                _context.Pedidos.Update(pedido);
                _context.Produtos.Update(produto); 

                await _context.SaveChangesAsync();

                response.Dados = await _context.ItensPedidos
                    .Where(i => i.PedidoId == itemPedidoCriarDto.PedidoId)
                    .ToListAsync();

                response.Mensagem = "Item cadastrado com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<ItemPedidoModel>>> Editar(ItemPedidoEditarDto itemPedidoEditarDto)
        {
            ResponseModel<List<ItemPedidoModel>> response = new ResponseModel<List<ItemPedidoModel>>();

            try
            {
                var item = await _context.ItensPedidos
                    .Include(i => i.Produto)
                    .FirstOrDefaultAsync(i => i.Id == itemPedidoEditarDto.Id);

                if(item == null)
                {
                    response.Mensagem = "Esse item não existe!";
                    response.Status = false;
                    return response;
                }

                if(itemPedidoEditarDto.Quantidade > item.Produto.Estoque)
                {
                    response.Mensagem = "Estoque ultrapassado!";
                    response.Status = false;
                    return response;
                }

                var pedido = await _context.Pedidos.FindAsync(item.PedidoId);

                if(pedido == null)
                {
                    response.Mensagem = "Pedido inexistente";
                    response.Status = false;
                    return response;
                }

                pedido.Total -= item.SubTotal;

                item.Quantidade = itemPedidoEditarDto.Quantidade;
                item.SubTotal = itemPedidoEditarDto.Quantidade * item.ValorUnitario;

                pedido.Total += item.SubTotal;

                _context.ItensPedidos.Update(item);
                _context.Pedidos.Update(pedido);
                await _context.SaveChangesAsync();

                response.Dados = await _context.ItensPedidos
                    .Where(i => i.PedidoId == item.PedidoId)
                    .ToListAsync();

                response.Mensagem = "Item alterado com sucesso!";
                return response;
                
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<ItemPedidoModel>>> Excluir(int id)
        {
            ResponseModel<List<ItemPedidoModel>> response = new ResponseModel<List<ItemPedidoModel>>();
            try
            {
                var item = await _context.ItensPedidos.FindAsync(id);

                if(item == null)
                {
                    response.Mensagem = "Esse item não existe!";
                    response.Status = false;
                    return response;
                }

                var pedido = await _context.Pedidos.FindAsync(item.PedidoId);
                var produto = await _context.Produtos.FindAsync(item.ProdutoId);

                pedido.Total -= item.SubTotal;
                produto.Estoque += item.Quantidade;

                _context.ItensPedidos.Remove(item);
                _context.Pedidos.Update(pedido);
                _context.Produtos.Update(produto);
                await _context.SaveChangesAsync();

                response.Dados = await _context.ItensPedidos
                    .Include(i => i.Produto)
                    .Include(i => i.Pedido.Cliente)
                    .ToListAsync();

                response.Mensagem = "Item excluido com sucesso!";
                return response;

            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<ItemPedidoModel>>> ListarItens()
        {
            ResponseModel<List<ItemPedidoModel>> response = new ResponseModel<List<ItemPedidoModel>>();
            try
            {

                var itens = await _context.ItensPedidos
                    .Include(i => i.Produto)
                    .Include(i => i.Pedido.Cliente)
                    .ToListAsync();

                if (!itens.Any())
                {
                    response.Mensagem = "Nenhum item cadastrado";
                    response.Status = false;
                    return response;
                }

                response.Dados = itens;
                response.Mensagem = "Todos os itens listados!";
                return response;

            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }
    }
}
