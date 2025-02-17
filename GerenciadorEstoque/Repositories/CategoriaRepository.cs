using System.Collections.Generic;
using System.Linq;
using GerenciadorEstoque.Models;

namespace GerenciadorEstoque.Repositories
{

    public static class CategoriaRepository
    {
        private static List<Categoria> categorias = new List<Categoria>();
        private static int proximoId = 1;

         public static void Adicionar(Categoria categoria)
        {
            categoria.Id = categorias.Count + 1;  // Definindo um Id simples baseado na quantidade de categorias
            categorias.Add(categoria);
        }



        public static List<Categoria> ListarTodas()
        {
            return categorias;
        }

        public static Categoria BuscarPorId(int id)
        {
            return categorias.FirstOrDefault(c => c.Id == id);
        }

        public static void Atualizar(Categoria categoria)
        {
            var categoriaExistente = BuscarPorId(categoria.Id);
            if (categoriaExistente != null)
            {
                categoriaExistente.Nome = categoria.Nome;
            }
        }

        public static void Remover(int id)
        {
            var categoria = BuscarPorId(id);
            if (categoria != null)
            {
                categorias.Remove(categoria);
            }
        }
    }
}
