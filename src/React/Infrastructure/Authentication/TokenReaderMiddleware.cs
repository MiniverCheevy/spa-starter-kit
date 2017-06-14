using System;
using System.Threading.Tasks;
using Core.Identity;
using Core.Security;
using Microsoft.AspNetCore.Http;

namespace Web.Infrastructure.Authentication
{
    public class TokenReaderMiddleware
    {
        private readonly RequestDelegate next;

        public TokenReaderMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.IsSecureRequest())
            {
                var key = "Token";
                var headers = context.Request.Headers;
                if (headers.ContainsKey(key))
                {
                    var token = headers[key].ToString();
                    var decrypted = Encryption.Decrypt<AppPrincipal>(token);
                    if (decrypted.Expiration < DateTime.Now)
                        context.Items[RequestContextProvider.AppPrincipal] = decrypted;
                }
            }
            await next(context);
        }
    }
}