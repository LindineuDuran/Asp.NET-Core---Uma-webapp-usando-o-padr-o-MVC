using ListaLeitura.App.Logica;
using ListaLeitura.App.MVC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

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
            builder.MapRoute("{classe}/{metodo}", RoteamentoPadrao.TratamentoPadrao);
            //builder.MapRoute("/Livros/ParaLer", LivrosLogica.ParaLer);
            //builder.MapRoute("/Livros/Lendo", LivrosLogica.Lendo);
            //builder.MapRoute("/Livros/Lidos", LivrosLogica.Lidos);
            //builder.MapRoute("/Livros/Detalhes/{id:int}", LivrosLogica.Detalhes);
            //builder.MapRoute("/Cadastro/NovoLivro/{Nome}/{Autor}", CadastroLogica.NovoLivro);
            //builder.MapRoute("/Cadastro/ExibeFormulario", CadastroLogica.ExibeFormulario);
            //builder.MapRoute("/Cadastro/Incluir", CadastroLogica.ProcessaFormulario);

            var rotas = builder.Build();
            app.UseRouter(rotas);
        }
    }
}
