using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ListaLeitura.App.MVC
{
    class RoteamentoPadrao
    {
        public static Task TratamentoPadrao(HttpContext context)
        {
            //rota padrão: /<Classe>Logica/Metodo
            //ex.: /{Classe}/{Metodo}
            var classe = context.GetRouteValue("classe").ToString();
            var nomeMetodo = context.GetRouteValue("metodo").ToString();

            var nomeCompletoClasse = $"ListaLeitura.App.Logica.{classe}Logica";

            var tipoClasse = Type.GetType(nomeCompletoClasse);
            var metodo = tipoClasse.GetMethods().Where(m => m.Name == nomeMetodo).First();

            var requestDelegate = (RequestDelegate) Delegate.CreateDelegate(typeof(RequestDelegate), metodo);

            return requestDelegate(context);
        }
    }
}
