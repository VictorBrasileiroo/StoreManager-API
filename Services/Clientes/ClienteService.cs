using Microsoft.EntityFrameworkCore;
using StoreManager.API.Data;
using StoreManager.API.Dto.Cliente;
using StoreManager.API.Models;
using BrazilianUtils;

namespace StoreManager.API.Services.Clientes
{
    public class ClienteService : IClienteInterface
    {
        private readonly AppDbContext _context;
        public ClienteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<List<ClientModel>>> AdicionarCliente(ClienteCriarDto clienteCriarDto)
        {
            ResponseModel<List<ClientModel>> response = new ResponseModel<List<ClientModel>>();
            try
            {
                //antes de cadastrar precisamos antes verificar se o cpf do dto está válido!

                bool cpfValido = BrazilianUtils.Cpf.IsValid(clienteCriarDto.Cpf);

                if (!cpfValido)
                {
                    response.Mensagem = "CPF inválido!";
                    response.Status = false;
                    return response;
                }

                clienteCriarDto.Cpf = Cpf.Format(clienteCriarDto.Cpf);

                //verificar se já existe alguem com esse cpf no banco
                var cpfExist = await _context.Clientes.FirstOrDefaultAsync(c => c.Cpf == clienteCriarDto.Cpf);

                if(cpfExist != null)
                {
                    response.Mensagem = "Já existe alguém com esse CPF cadastrado!";
                    response.Status = false;
                    return response;
                }

                var newCliente = new ClientModel()
                {
                    Nome = clienteCriarDto.Nome,
                    Cpf = clienteCriarDto.Cpf,
                };

                _context.Add(newCliente);
                await _context.SaveChangesAsync();

                response.Dados = await _context.Clientes.ToListAsync();
                response.Mensagem = "Cliente cadastrado com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<ClientModel>> BuscarClientePorId(int idCliente)
        {
            ResponseModel<ClientModel> response = new ResponseModel<ClientModel>();
            try
            {
                var cliente  = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == idCliente);

                if (cliente == null)
                {
                    response.Mensagem = "Cliente não encontrado!";
                    response.Status = false;
                    return response;
                }

                response.Dados = cliente;
                response.Mensagem = "Cliente localizado com sucesso!";
                return response;

            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<ClientModel>>> EditarCliente(ClienteEditarDto clienteEditarDto)
        {
            ResponseModel<List<ClientModel>> response = new ResponseModel<List<ClientModel>>();
            try
            {
               var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == clienteEditarDto.Id);

                if (cliente == null)
                {
                    response.Mensagem = "Nenhum cliente localizado!";
                    response.Status = false;
                    return response;
                }

                 if(clienteEditarDto.Cpf != "string")
                 {
                    //validar o cpf!
                    bool cpfValido = BrazilianUtils.Cpf.IsValid(clienteEditarDto.Cpf);

                    if (!cpfValido)
                    {
                        response.Mensagem = "CPF inválido!";
                        response.Status = false;
                        return response;
                    }

                    clienteEditarDto.Cpf = Cpf.Format(clienteEditarDto.Cpf);

                    //verificar se já existe alguem com esse cpf no banco
                    var cpfExist = await _context.Clientes.FirstOrDefaultAsync(c => c.Cpf == clienteEditarDto.Cpf);

                    if (cpfExist != null)
                    {
                        response.Mensagem = "Já existe alguém com esse CPF cadastrado!";
                        response.Status = false;
                        return response;
                    }

                    cliente.Cpf = clienteEditarDto.Cpf;
                }

                 if(clienteEditarDto.Nome != "string")
                {
                    cliente.Nome = clienteEditarDto.Nome;
                }
        

                _context.Update(cliente);
                await _context.SaveChangesAsync();

                response.Dados = await _context.Clientes.ToListAsync();
                response.Mensagem = "Cliente editado com sucesso!";
                return response;

            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<ClientModel>>> ExcluirCliente(int idCliente)
        {
            ResponseModel<List<ClientModel>> response = new ResponseModel<List<ClientModel>>();
            try
            {
                var cliente = await _context.Clientes
                    .Include(c => c.Pedidos)
                    .FirstOrDefaultAsync(c => c.Id == idCliente);

                if (cliente == null)
                {
                    response.Mensagem = "Cliente não localizado!";
                    response.Status = false;
                    return response;
                }

                _context.Pedidos.RemoveRange(cliente.Pedidos);
                _context.Remove(cliente);
                await _context.SaveChangesAsync();

                response.Dados = await _context.Clientes.ToListAsync();
                response.Mensagem = "Cliente excluido com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<ClientModel>>> ListarClientes()
        {
            ResponseModel<List<ClientModel>> response = new ResponseModel<List<ClientModel>>();
            try
            {
                var ListaClientes = await _context.Clientes.ToListAsync();

                if(!ListaClientes.Any())
                {
                    response.Mensagem = "Nenhum cliente foi cadastrado!";
                    response.Status = false;
                    return response;
                }

                response.Dados = ListaClientes;
                response.Mensagem = "Listagem realizada com sucesso!";
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
