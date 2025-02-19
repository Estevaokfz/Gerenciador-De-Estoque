using System.Collections.Generic;
using System.Linq;
using GerenciadorEstoque.Models;

namespace GerenciadorEstoque.Repositories
{
    public static class ProdutoRepository
    {
        private static List<Produto> produtos = new List<Produto>(); // Lista que armazena os produtos

        public static void Adicionar(Produto produto)
        {
            produto.Id = produtos.Count > 0 ? produtos.Max(p => p.Id) + 1 : 1; // Gera um novo ID
            produtos.Add(produto);
        }

        public static List<Produto> ObterTodos()
        {
            return produtos;
        }

        public static Produto ObterPorId(int id)
        {
            return produtos.FirstOrDefault(p => p.Id == id);
        }

        public static void Atualizar(Produto produto)
        {
            var existente = ObterPorId(produto.Id);
            if (existente != null)
            {
                existente.Nome = produto.Nome;
                existente.Categoria = produto.Categoria;
            }
        }

        public static void Remover(int id)
        {
            var produto = ObterPorId(id);
            if (produto != null)
            {
                produtos.Remove(produto);
            }
        }
    }
}
