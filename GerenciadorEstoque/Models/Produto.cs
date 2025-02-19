using System;

namespace GerenciadorEstoque.Models
{

    public class Produto
    {
       public int Id { get; set; }
        public string Nome { get; set; }
        public Categoria Categoria { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor
        {
            get => _Valor ;
            set
            {
                if (value < 0) throw new ArgumentException("O preço não pode ser negativo.");
                _Valor  = value;
            }
        }
        private decimal _Valor ;
    }
}