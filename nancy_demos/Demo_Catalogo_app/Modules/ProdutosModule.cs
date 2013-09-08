using System;
using Nancy;
using Nancy.ModelBinding;
using System.Collections.Generic;
using Demo_Catalogo_app.Models;

namespace Demo_Catalogo_app.Modules
{
    public class ProdutosModule : NancyModule
    {
        private static List<Produto> _produtos;

        public ProdutosModule()
        {
            CarregaProdutos();

            Get["/produtos"] = _ =>
            {
                return View["Views/Produtos", _produtos];
            };

            Get["/produto/novo"] = _ =>
            {
                return View["Views/EditProduto", new Produto() { Preco = 1.99m }];
            };

            Post["/produto/novo"] = _ =>
            {
                Produto produto = this.BindTo(new Produto());
                _produtos.Add(produto);

                return Response.AsRedirect("/produtos");
            };
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