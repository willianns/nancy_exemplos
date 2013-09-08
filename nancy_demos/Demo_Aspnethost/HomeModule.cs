using Nancy;
using System;
using System.Web;

namespace Demo_Aspnethost
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => "ASP.NET Hosting up and running!!!";
        }
    }
}