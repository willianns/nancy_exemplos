using System;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.ModelBinding;

namespace Demo_Catalogo_app.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => View["Views/Index"];

            Get["/autenticar"] = _ =>
            {
                return View["Views/Autenticar", ""];
            };

            Post["/autenticar"] = _ =>
            {
                var login = this.BindTo(new LoginForm());
                //Simples demonstração com valores fixos
                if (login.Usuario != "jose" || login.Senha != "123")
                {
                    return View["Views/Autenticar", "Credênciais inválidas"];
                }

                return this.LoginAndRedirect(Guid.Parse("4AAF4DCC-65C6-4CAD-9545-3AD33E8C7289"), fallbackRedirectUrl: "/");
            };

            Get["/sair"] = _ => this.LogoutAndRedirect("/");
        }

        public class LoginForm
        {
            public string Usuario { get; set; }
            public string Senha { get; set; }
        }
    }
}