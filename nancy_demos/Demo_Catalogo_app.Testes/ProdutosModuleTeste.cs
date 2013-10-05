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
    public class ProdutosModuleTeste
    {
        Browser browser;

        [TestInitialize]
        public void Initialize()
        {
            var bootstrapper = new ConfigurableBootstrapper((cfg) =>
            {
                cfg.Module<HomeModule>();
                cfg.Module<ProdutosModule>();
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
        public void Get_Produtos_Nao_Autenticado_Deve_Ser_Redirecionado_Ao_Login()
        {
            var response = browser.Get("/produtos");

            response.ShouldHaveRedirectedTo("/autenticar?returnUrl=/produtos");
        }

        [TestMethod]
        public void Get_Produtos()
        {
            RealizarLogin();

            var response = browser.Get("/produtos");

            response.Body["h3"].ShouldContain("Listando produtos");
        }

        [TestMethod]
        public void Post_Produto_Novo_Valido()
        {
            RealizarLogin();

            var response = browser.Post("/produto/novo", (with) =>  {
                with.FormValue("Descricao", "Novo produto teste");
                with.FormValue("Preco", "10,00");
            });

            response.ShouldHaveRedirectedTo("/produtos");
        }

        [TestMethod]
        public void Post_Produto_Novo_Invalido()
        {
            RealizarLogin();

            var response = browser.Post("/produto/novo", (with) => {
                with.FormValue("Descricao", "");
                with.FormValue("Preco", "");
            });

            response.Body["ul li.erro"].ShouldExist();
        }

        private void RealizarLogin()
        {
            browser.Post("/autenticar", (with) => {
                with.FormValue("Usuario", "jose");
                with.FormValue("Senha", "123");
            });
        }
    }

    public class FakeCsrfTokenValidator : ICsrfTokenValidator
    {

        public bool CookieTokenStillValid(CsrfToken cookieToken)
        {
            return true;
        }

        public CsrfTokenValidationResult Validate(CsrfToken tokenOne, CsrfToken tokenTwo, TimeSpan? validityPeriod = null)
        {
            return CsrfTokenValidationResult.Ok;
        }
    }
}
