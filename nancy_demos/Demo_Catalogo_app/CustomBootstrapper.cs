using Demo_Catalogo_app.Models;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Conventions;
using System;

namespace Demo_Catalogo_app
{
    public class CustomBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureRequestContainer(Nancy.TinyIoc.TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);

            container.Register<IUserMapper, UsuarioMapper>();
        }

        protected override void RequestStartup(Nancy.TinyIoc.TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);

            var formsConfig = new FormsAuthenticationConfiguration() 
            {
                RedirectUrl = "~/autenticar",
                UserMapper = container.Resolve<IUserMapper>()
            };

            FormsAuthentication.Enable(pipelines, formsConfig);
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);

            nancyConventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory("assets", @"public")
            );
        }
    }
}