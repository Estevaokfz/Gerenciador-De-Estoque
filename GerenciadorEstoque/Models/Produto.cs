using System;

namespace GerenciadorEstoque.Models
{

    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco
        {
            get => _preco;
            set
            {
                if (value < 0) throw new ArgumentException("O preço não pode ser negativo.");
                _preco = value;
            }
        }
        private decimal _preco;
    }
}