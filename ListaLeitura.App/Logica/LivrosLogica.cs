using ListaLeitura.App.html;
using ListaLeitura.App.Negocio;
using ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaLeitura.App.Logica
{
    class LivrosLogica
    {
        private static string CarregaLista(IEnumerable<Livro> livros)
        {
            var html = HtmlUtils.CarregaArquivoHTML("lista");

            foreach (var livro in livros)
            {
                html = html.Replace("#NOVO-ITEM", $"<li>{livro.Titulo} - {livro.Autor}</li>#NOVO-ITEM");
            }

            return html = html.Replace("#NOVO-ITEM", "");
        }

        public static Task LivrosParaLer(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            var html = CarregaLista(_repo.ParaLer.Livros);

            return context.Response.WriteAsync(html);
        }
        public static Task LivrosLendo(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            var html = CarregaLista(_repo.Lendo.Livros);

            return context.Response.WriteAsync(html);
        }
        public static Task LivrosLidos(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            var html = CarregaLista(_repo.Lidos.Livros);

            return context.Response.WriteAsync(html);
        }

        public static Task ExibeDetalhes(HttpContext context)
        {
            int id = Convert.ToInt32(context.GetRouteValue("id"));

            var repo = new LivroRepositorioCSV();
            var livro = repo.Todos.FirstOrDefault(l => l.Id == id);

            return context.Response.WriteAsync(livro.Detalhes());
        }
    }
}
