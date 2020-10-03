using ListaLeitura.App.Negocio;
using ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ListaLeitura.App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app)
        {
            var builder = new RouteBuilder(app);
            builder.MapRoute("/Livros/ParaLer", LivrosParaLer);
            builder.MapRoute("/Livros/Lendo", LivrosLendo);
            builder.MapRoute("/Livros/Lidos", LivrosLidos);
            builder.MapRoute("/Cadastro/NovoLivro/{Nome}/{Autor}", NovoLivroParaLer);
            builder.MapRoute("/Cadastro/NovoLivro", ExibeFormulario);
            builder.MapRoute("/Cadastro/Incluir", ProcessaFormulario);
            builder.MapRoute("/Livros/Detalhes/{id:int}", ExibeDetalhes);

            var rotas = builder.Build();
            app.UseRouter(rotas);

            //app.Run(Roteamento);
        }

        public Task LivrosParaLer(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            var html = CarregaArquivoHtml("ParaLer");

            foreach(var livro in _repo.ParaLer.Livros)
            {
                html = html.Replace("#NOVO-ITEM", $"<li>{livro.Titulo} - {livro.Autor}</li>#NOVO-ITEM");
            }

            html = html.Replace("#NOVO-ITEM", "");

            return context.Response.WriteAsync(html);
        }
        public Task LivrosLendo(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            var html = CarregaArquivoHtml("Lendo");

            foreach (var livro in _repo.Lendo.Livros)
            {
                html = html.Replace("#NOVO-ITEM", $"<li>{livro.Titulo} - {livro.Autor}</li>#NOVO-ITEM");
            }

            html = html.Replace("#NOVO-ITEM", "");

            return context.Response.WriteAsync(html);
        }
        public Task LivrosLidos(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            var html = CarregaArquivoHtml("Lidos");

            foreach (var livro in _repo.Lidos.Livros)
            {
                html = html.Replace("#NOVO-ITEM", $"<li>{livro.Titulo} - {livro.Autor}</li>#NOVO-ITEM");
            }

            html = html.Replace("#NOVO-ITEM", "");

            return context.Response.WriteAsync(html);
        }

        private Task ExibeFormulario(HttpContext context)
        {
            var html = CarregaArquivoHtml("Formulario");

            return context.Response.WriteAsync(html);
        }

        private string CarregaArquivoHtml(string nomeArquivo)
        {
            var nomeCompletoArquivo = $"html/{nomeArquivo}.html";
            using (var arquivo = File.OpenText(nomeCompletoArquivo))
            {
                return arquivo.ReadToEnd();
            }
        }

        public Task ProcessaFormulario(HttpContext context)
        {
            var livro = new Livro
            {
                Titulo = context.Request.Form["titulo"].First(),
                Autor = context.Request.Form["autor"].First()
            };

            var repo = new LivroRepositorioCSV();
            repo.Incluir(livro);

            return context.Response.WriteAsync("Livro incluido com sucesso!!!");
        }

        public Task NovoLivroParaLer(HttpContext context)
        {
            var livro = new Livro
            {
                Titulo = context.GetRouteValue("titulo").ToString(),
                Autor = context.GetRouteValue("autor").ToString()
            };

            var repo = new LivroRepositorioCSV();
            repo.Incluir(livro);

            return context.Response.WriteAsync("Livro incluido com sucesso!!!");
        }

        private Task ExibeDetalhes(HttpContext context)
        {
            int id = Convert.ToInt32(context.GetRouteValue("id"));

            var repo = new LivroRepositorioCSV();
            var livro = repo.Todos.FirstOrDefault(l => l.Id == id);

            return context.Response.WriteAsync(livro.Detalhes());
        }
    }
}
