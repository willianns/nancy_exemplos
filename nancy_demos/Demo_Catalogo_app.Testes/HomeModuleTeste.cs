using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy.Testing;
using Nancy;
using Demo_Catalogo_app.Modules;
using Nancy.Authentication.Forms;
using Demo_Catalogo_app.Models;
using Nancy.Security;

namespace Demo_Catalogo_app.Testes
{
    [TestClass]
    public class HomeModuleTeste
    {
        Browser browser;

        [TestInitialize]
        public void Initialize()
        {
            var bootstrapper = new ConfigurableBootstrapper((cfg) =>
            {
                cfg.Module<HomeModule>();
                cfg.Dependency<IUserMapper>(new UsuarioMapper());
                cfg.CsrfTokenValidator(new FakeCsrfTokenValidator());
                cfg.ApplicationStartup((container, pipelines) =>
                {
                    Csrf.Enable(pipelines);
                    var formsConfig = new FormsAuthenticationConfiguration()
                    {
                        RedirectUrl = "~/autenticar",
                        UserMapper = container.Resolve<IUserMapper>()
                    };

                    FormsAuthentication.Enable(pipelines, formsConfig);
                });
            });

            browser = new Browser(bootstrapper);
        }

        [TestMethod]
        public void Get_Home_Index()
        {
            var response = browser.Get("/");

            response.Body["h4"].ShouldContain("Bem vindo ao catalogo de produtos!");
        }

        [TestMethod]
        public void Get_Home_Autenticar()
        {
            var response = browser.Get("/autenticar");

            response.Body["h2"].ShouldContain("Login");
            response.Body["form"].ShouldExist();
        }

        [TestMethod]
        public void Post_Home_Autenticar_Valido()
        {
            var response = browser.Post("/autenticar", (with) => {
                with.FormValue("Usuario", "jose");
                with.FormValue("Senha", "123");
            });

            response.ShouldHaveRedirectedTo("/");
        }

        [TestMethod]
        public void Post_Home_Autenticar_Invalido()
        {
            var response = browser.Post("/autenticar", (with) => {
                with.FormValue("Usuario", "baduser");
                with.FormValue("Senha", "badpass");
            });

            response.Body["p:first"].ShouldContain("Cred&#234;nciais inv&#225;lidas");
        }
    }
}
