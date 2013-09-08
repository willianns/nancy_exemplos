using Nancy;
using System;

namespace Demo_Selfhost
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = p => "Olá!!";

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
                return String.Format("Pagina acessada via POST, nome postado {0}", Request.Form["nome"]);
            };
        }
    }
}
