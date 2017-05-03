using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Voodoo;

namespace React.Infrastructure
{
    public static class ContextExtensions
    {
        public static bool IsSecureRequest(this HttpContext context)
        {
            return context.Request.Path.To<string>().ToLower().Contains(@"/api/");
        }
    }
}
