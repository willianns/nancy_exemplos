using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Validation;
using Nancy.Security;
using Demo_Catalogo_app.Models;

namespace Demo_Catalogo_app.Modules
{
    public class ProdutosModule : NancyModule
    {
        private static List<Produto> _produtos;

        public ProdutosModule()
        {
            this.RequiresAuthentication();
            this.RequiresClaims(new[] { "admin" });

            //produtos fake
            CarregaProdutos();

            Get["/produtos"] = _ =>
            {
                return View["Views/Produtos", _produtos];
            };

            Get["/produto/novo"] = _ =>
            {
                return View["Views/EditProduto", new { 
                    Erros = new string[0],
                    Produto = new Produto() { Preco = 1.99m } 
                }];
            };

            Post["/produto/novo"] = _ =>
            {
                this.ValidateCsrfToken();

                Produto produto = this.BindTo(new Produto());

                ModelValidationResult result = this.Validate(produto);

                if (result.IsValid)
                {
                    _produtos.Add(produto);
                    return Response.AsRedirect("/produtos");
                }
                else
                {
                    return View["Views/EditProduto", new { 
                            Erros = ObterErros(result), 
                            Produto = produto 
                    }];
                }
            };
        }

        private IEnumerable<string> ObterErros(ModelValidationResult result)
        {
            return from e in result.Errors
                   select e.GetMessage(e.MemberNames.First());
        }

        private void CarregaProdutos()
        {
            if (_produtos == null)
            {
                _produtos = new List<Produto>();
                _produtos.Add(new Produto { Descricao = "Boné Trucker", Preco = 120.00m });
                _produtos.Add(new Produto { Descricao = "Tênis p/ Skate", Preco = 349.99m });
            }
        }
    }
}