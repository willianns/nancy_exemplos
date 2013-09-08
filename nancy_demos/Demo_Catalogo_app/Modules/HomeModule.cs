using System;
using Nancy;

namespace Demo_Catalogo_app.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => View["Views/Index"];
        }
    }
}