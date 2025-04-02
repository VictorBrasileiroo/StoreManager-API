using Microsoft.EntityFrameworkCore;
using StoreManager.API.Data;
using StoreManager.API.Dto.Produto;
using StoreManager.API.Models;

namespace StoreManager.API.Services.Produtos
{
    public class ProdutoService : IProdutoInterface
    {
        private readonly AppDbContext _context;
        public ProdutoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<ProdutoModel>> AdicionarProduto(ProdutoCriarDto produtoCriarDto)
        {
            ResponseModel<ProdutoModel> response = new ResponseModel<ProdutoModel>();
            try
            {

                var novoProduto = new ProdutoModel()
                {
                    Nome = produtoCriarDto.Nome,
                    Descricao = produtoCriarDto.Descricao,
                    Estoque = produtoCriarDto.Estoque,
                    Preco = produtoCriarDto.Preco
                };

                _context.Add(novoProduto);
                await _context.SaveChangesAsync();

                response.Dados = novoProduto;
                response.Mensagem = "Produto Adicionado com sucesso!";
                return response;

            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<ProdutoModel>> BuscarProdutoPorId(int idProduto)
        {
            ResponseModel<ProdutoModel> response = new ResponseModel<ProdutoModel>();
            try
            {
                var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == idProduto);

                if (produto == null)
                {
                    response.Mensagem = "Produto não encontrado!";
                    response.Status = false;
                    return response;
                }

                response.Dados = produto;
                response.Mensagem = "Produto localizado com sucesso!";
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<ProdutoModel>>> EditarProduto(ProdutoEditarDto produtoEditarDto)
        {
            ResponseModel<List<ProdutoModel>> response = new ResponseModel<List<ProdutoModel>>();
            try
            {
                var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == produtoEditarDto.Id);

                if (produto == null)
                {
                    response.Mensagem = "Produto não Localizado!";
                    response.Status = false;
                    return response;
                }

                if (!string.IsNullOrEmpty(produtoEditarDto.Nome) && produtoEditarDto.Nome != "string")
                    produto.Nome = produtoEditarDto.Nome;

                if (!string.IsNullOrEmpty(produtoEditarDto.Descricao) && produtoEditarDto.Descricao != "string")
                    produto.Descricao = produtoEditarDto.Descricao;

                if (produtoEditarDto.Preco > 0)
                    produto.Preco = produtoEditarDto.Preco;

                if (produtoEditarDto.Estoque > 0)
                    produto.Estoque = produtoEditarDto.Estoque;

                _context.Update(produto);
                await _context.SaveChangesAsync();

                response.Dados = await _context.Produtos.ToListAsync();
                response.Mensagem = "Produto Editado com sucesso!";
                return response;

            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<ProdutoModel>>> ExcluirProduto(int idProduto)
        {
            ResponseModel<List<ProdutoModel>> response = new ResponseModel<List<ProdutoModel>>();
            try
            {

                var produtoExcluir = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == idProduto);

                if(produtoExcluir == null)
                {
                    response.Mensagem = "Produto não localizado!";
                    response.Status = false;
                    return response;
                }

                _context.Remove(produtoExcluir);
                await _context.SaveChangesAsync();

                response.Dados = await _context.Produtos.ToListAsync();
                response.Mensagem = "Produto excluido com sucesso!";
                return response;

            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<ProdutoModel>>> ListarProdutos()
        {
            ResponseModel<List<ProdutoModel>> response = new ResponseModel<List<ProdutoModel>>();
            try
            {

                var produtos = await _context.Produtos.ToListAsync();

                if (produtos == null)
                {
                    response.Mensagem = "Nenhum produto cadastrado!";
                    response.Status = false;
                    return response;
                }
                else
                {
                    response.Dados = produtos;
                    response.Mensagem = "Produtos listados com sucesso!";
                    return response;
                }   
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<ProdutoModel>>> ListarProdutosPorFaixaPreco(decimal faixaMinimo, decimal faixaMaximo)
        {
            ResponseModel<List<ProdutoModel>> response = new ResponseModel<List<ProdutoModel>>();
            try
            {
                //criar o query
                var query = _context.Produtos.AsQueryable();

                if(faixaMinimo > 0)
                {
                    query = query.Where(c => c.Preco >= faixaMinimo);
                }

                if (faixaMaximo > 0)
                {
                    query = query.Where(c => c.Preco <= faixaMaximo);
                }

                query = query.OrderBy(c => c.Preco);

                var produtos = await query.ToListAsync();
                
                if(produtos == null && produtos.Count == 0)
                {
                    response.Mensagem = "Nenhum Produto nessa faixa de preço!";
                    response.Status = false;
                    return response;
                }
                else
                {
                    response.Dados = produtos;
                    response.Mensagem = "Produtos listados por faixa de preço com sucesso!";
                    response.Status = true;
                    return response;
                }
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
