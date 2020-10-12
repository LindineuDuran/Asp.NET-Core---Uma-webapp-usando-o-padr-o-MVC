using ListaLeitura.App.html;
using ListaLeitura.App.Negocio;
using ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Linq;
using System.Threading.Tasks;

namespace ListaLeitura.App.Logica
{
    public class CadastroLogica
    {
        public static Task ExibeFormulario(HttpContext context)
        {
            var html = HtmlUtils.CarregaArquivoHTML("Formulario");

            return context.Response.WriteAsync(html);
        }

        public static Task ProcessaFormulario(HttpContext context)
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

        public static Task NovoLivro(HttpContext context)
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
    }
}
