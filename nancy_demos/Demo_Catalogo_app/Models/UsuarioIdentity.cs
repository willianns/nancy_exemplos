using Nancy.Security;
using System;
using System.Collections.Generic;

namespace Demo_Catalogo_app.Models
{
    public class UsuarioIdentity : IUserIdentity
    {
        public string UserName { get; set; }

        public IEnumerable<string> Claims { get; set; }
    }
}