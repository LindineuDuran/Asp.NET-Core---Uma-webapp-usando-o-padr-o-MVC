using ListaLeitura.App.Negocio;
using ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

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
            return context.Response.WriteAsync(_repo.ParaLer.ToString());
        }
        public Task LivrosLendo(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            return context.Response.WriteAsync(_repo.Lendo.ToString());
        }
        public Task LivrosLidos(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            return context.Response.WriteAsync(_repo.Lidos.ToString());
        }

        private Task ExibeFormulario(HttpContext context)
        {
            var html = @"<html>
                <form action = '/Cadastro/Incluir'>
                    <input name = 'titulo'/>
                    <input name = 'autor'/>
                    <button>Gravar</button>
                </form>
            </html>";

            return context.Response.WriteAsync(html);
        }
        public Task ProcessaFormulario(HttpContext context)
        {
            var livro = new Livro
            {
                Titulo = context.Request.Query["titulo"].First(),
                Autor = context.Request.Query["autor"].First()
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
