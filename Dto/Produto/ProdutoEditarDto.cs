﻿namespace StoreManager.API.Dto.Produto
{
    public class ProdutoEditarDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
    }
}
