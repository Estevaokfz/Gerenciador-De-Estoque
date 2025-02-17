using System.Collections.Generic;
using System.Linq;
using GerenciadorEstoque.Models;

namespace GerenciadorEstoque.Repositories
{
    public static class ProdutoRepository
    {
        private static List<Produto> produtos = new List<Produto>();
        private static int proximoId = 1;

        public static void Adicionar(Produto produto)
        {
            produto.Id = proximoId++;
            produtos.Add(produto);
        }

        public static List<Produto> ListarTodos()
        {
            return produtos.ToList();
        }

        public static Produto BuscarPorId(int id)
        {
            return produtos.FirstOrDefault(p => p.Id == id);
        }

        public static void Atualizar(Produto produto)
        {
            var produtoExistente = BuscarPorId(produto.Id);
            if (produtoExistente != null)
            {
                produtoExistente.Nome = produto.Nome;
                produtoExistente.Categoria = produto.Categoria;
                produtoExistente.Quantidade = produto.Quantidade;
                produtoExistente.Preco = produto.Preco;
            }
        }

        public static void Remover(int id)
        {
            var produto = BuscarPorId(id);
            if (produto != null)
            {
                produtos.Remove(produto);
            }
        }
        
    }
}

