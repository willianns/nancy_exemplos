using System;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Security;
using System.Collections.Generic;

namespace Demo_Catalogo_app.Models
{
    public class UsuarioMapper : IUserMapper
    {
        public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
            if (Guid.Parse("4AAF4DCC-65C6-4CAD-9545-3AD33E8C7289") == identifier)
            {
                return new UsuarioIdentity
                {
                    UserName = "jose",
                    Claims = new [] { "admin" }
                };
            }

            return null;
        }
    }
}