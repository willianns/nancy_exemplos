using Nancy;
using System;
using System.Diagnostics;

namespace Demo_Selfhost
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Before += ctx =>
            {
                Trace.WriteLine(String.Format("Url requisitada = {0}", ctx.Request.Url));
                return null;
            };

            After += ctx =>
            {
                Trace.WriteLine("Após processamento");
            };

            Get["/"] = p => "Nancy up and running!";

            Get["/hi/{name}"] = p => 
            {
                return String.Format("Hello {0}", p.name);
            };

            Get["/form"] = p =>
            {
                return @"
                <html>
                   <head><meta charset='utf-8' /></head>
                   <body>
                      <form method='post' action='/form'>
                        <input type='text' name='nome'>
                        <input type='submit' value='requisição post'>
                      </form>        
                   </body>
                </html>";
            };

            Post["/form"] = p =>
            {
                return String.Format("Pagina acessada via POST, valor postado {0}", Request.Form["nome"]);
            };

            Get["/negotiation"] = _ => Negotiate.WithModel(new { Mensagem = "Hello" }).WithView("Msg");
        }
    }
}
