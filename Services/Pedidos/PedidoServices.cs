using Microsoft.EntityFrameworkCore;
using StoreManager.API.Data;
using StoreManager.API.Dto.Pedido;
using StoreManager.API.Models;

namespace StoreManager.API.Services.Pedidos
{
    public class PedidoServices : IPedidoInterface
    {
        private readonly AppDbContext _context;
        public PedidoServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<PedidoModel>> BuscarPorId(int id)
        {
            ResponseModel<PedidoModel> response = new ResponseModel<PedidoModel>();
            try
            {
                var pedido = await _context.Pedidos
                    .Include(p => p.Cliente)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (pedido == null)
                {
                    response.Mensagem = "Nenhum pedido localizado!";
                    response.Status = false;
                    return response;
                }

                response.Dados = pedido;
                response.Mensagem = "Pedido localizado com sucesso!";
                return response;

            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<PedidoModel>>> BuscarPorIdCliente(int idCliente)
        {
            ResponseModel<List<PedidoModel>> response = new ResponseModel<List<PedidoModel>>();
            try
            {

                var pedidos = await _context.Pedidos
                    .Include(p => p.Itens)
                    .Where(p => p.ClienteId == idCliente)
                    .ToListAsync();

                if (!pedidos.Any())
                {
                    response.Mensagem = "O cliente não possui pedidos!";
                    response.Status = false;
                    return response;
                }

                response.Dados = pedidos;
                response.Mensagem = "Pedidos listados com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<PedidoModel>> Criar(PedidoCriarDto pedidoCriarDto)
        {
            ResponseModel<PedidoModel> response = new ResponseModel<PedidoModel>();
            try
            {
                var cliente = await _context.Clientes.FindAsync(pedidoCriarDto.ClienteId);

                if (cliente == null)
                {
                    response.Mensagem = "Cliente não existe!";
                    response.Status = false;
                    return response;
                }

                //criar um novo pedido.
                var pedido = new PedidoModel()
                {
                    ClienteId = pedidoCriarDto.ClienteId,
                    Total = 0
                };

                _context.Add(pedido);
                await _context.SaveChangesAsync();

                response.Dados = pedido;
                response.Mensagem = "Pedido Cadastrado!";
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<PedidoModel>>> Excluir(int id)
        {
            ResponseModel<List<PedidoModel>> response = new ResponseModel<List<PedidoModel>>();
            try
            {
                var pedido = await _context.Pedidos.FindAsync(id);

                if (pedido == null)
                {
                    response.Mensagem = "Pedido não encontrado!";
                    response.Status = false;
                    return response;
                }

                _context.Remove(pedido);
                await _context.SaveChangesAsync();


                response.Dados = await _context.Pedidos
                    .Include(p => p.Cliente)
                    .ToListAsync();
                response.Mensagem = "Pedido excluido com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<PedidoModel>>> ListarPedidos()
        {
            ResponseModel<List<PedidoModel>> response = new ResponseModel<List<PedidoModel>>();
            try
            {
                var pedidos = await _context.Pedidos
                    .Include(p => p.Itens)
                    .Include(p => p.Cliente)
                    .ToListAsync();

                if(!pedidos.Any())
                {
                    response.Mensagem = "Nenhum pedido cadastrado!";
                    response.Status = false;
                    return response;
                }

                response.Dados = pedidos;
                response.Mensagem = "Todos os pedidos listados com sucesso!";
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
