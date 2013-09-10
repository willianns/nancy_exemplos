using Nancy;
using Nancy.Conventions;
using System;

namespace Demo_Catalogo_app
{
    public class CustomBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);

            nancyConventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory("assets", @"public")
            );
        }
    }
}