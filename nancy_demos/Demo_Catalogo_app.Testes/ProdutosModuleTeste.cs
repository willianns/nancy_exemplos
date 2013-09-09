using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy.Testing;
using Nancy;
using Demo_Catalogo_app.Modules;

namespace Demo_Catalogo_app.Testes
{
    [TestClass]
    public class ProdutosModuleTeste
    {
        Browser browser;
        ProdutosModule module;

        [TestInitialize]
        public void Init()
        {
            browser = new Browser(new ConfigurableBootstrapper((cfg) => {
                cfg.Module<ProdutosModule>();
            }));
        }

        [TestMethod]
        public void Get_Produtos()
        {
            var response = browser.Get("/produtos");

            response.Body["h3"].ShouldContain("Listando produtos");
        }

        [TestMethod]
        public void Post_Produto_Novo_Valido()
        {
            var response = browser.Post("/produto/novo", (with) =>  {
                with.FormValue("Descricao", "Novo produto teste");
                with.FormValue("Preco", "10,00");
            });

            response.ShouldHaveRedirectedTo("/produtos");
        }

        [TestMethod]
        public void Post_Produto_Novo_Invalido()
        {
            var response = browser.Post("/produto/novo", (with) =>
            {
                with.FormValue("Descricao", "");
                with.FormValue("Preco", "");
            });

            response.Body["ul li.erro"].ShouldExist();
        }
    }
}
