using StoreManager.API.Dto.Produto;
using StoreManager.API.Models;

namespace StoreManager.API.Services.Produtos
{
    public interface IProdutoInterface
    {
        //READ BÁSICO
        Task<ResponseModel<List<ProdutoModel>>> ListarProdutos();
        Task<ResponseModel<ProdutoModel>> BuscarProdutoPorId(int idProduto);

        //READ FILTRAGEM
        Task<ResponseModel<List<ProdutoModel>>> ListarProdutosPorFaixaPreco(decimal faixaMinimo, decimal faixaMaximo);

        //DELETE
        Task<ResponseModel<List<ProdutoModel>>> ExcluirProduto(int idProduto);

        //CREATE
        Task<ResponseModel<ProdutoModel>> AdicionarProduto(ProdutoCriarDto produtoCriarDto);

        //UPDATE
        Task<ResponseModel<List<ProdutoModel>>> EditarProduto(ProdutoEditarDto produtoEditarDto);

    }
}
