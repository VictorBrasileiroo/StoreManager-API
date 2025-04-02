using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManager.API.Dto.Produto;
using StoreManager.API.Models;
using StoreManager.API.Services.Produtos;

namespace StoreManager.API.Controllers
{
    [Route("api/produtos")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoInterface _produtoInterface;

        public ProdutoController(IProdutoInterface produtoInterface)
        {
            _produtoInterface = produtoInterface;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<ProdutoModel>>>> BuscarTodos()
        {
            var produtos = await _produtoInterface.ListarProdutos();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<ProdutoModel>>> BuscarPorId(int id)
        {
            var produto = await _produtoInterface.BuscarProdutoPorId(id);
            return Ok(produto);
        }

        [HttpGet("preco")]
        public async Task<ActionResult<ResponseModel<List<ProdutoModel>>>> BuscarPorFaixaPreco([FromQuery] decimal minimo, [FromQuery] decimal maximo)
        {
            var produtos = await _produtoInterface.ListarProdutosPorFaixaPreco(minimo, maximo);
            return Ok(produtos);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel<List<ProdutoModel>>>> Criar(ProdutoCriarDto produtoCriarDto)
        {
            var produtos = await _produtoInterface.AdicionarProduto(produtoCriarDto);
            return Ok(produtos);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseModel<List<ProdutoModel>>>> Atualizar(int id, ProdutoEditarDto produtoEditarDto)
        {
            if (id != produtoEditarDto.Id)
            {
                return BadRequest("O ID na URL não corresponde ao ID no objeto");
            }

            var produtos = await _produtoInterface.EditarProduto(produtoEditarDto);
            return Ok(produtos);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseModel<List<ProdutoModel>>>> Excluir(int id)
        {
            var produtos = await _produtoInterface.ExcluirProduto(id);
            return Ok(produtos);
        }
    }
}